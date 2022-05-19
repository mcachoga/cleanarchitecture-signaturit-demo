using Signaturit.Application.Features.Trials.Commands.Create;
using Signaturit.Application.Features.Trials.Commands.Delete;
using Signaturit.Application.Features.Trials.Commands.Update;
using Signaturit.Application.Features.Trials.Queries.GetAllCached;
using Signaturit.Application.Features.Trials.Queries.GetById;
using Signaturit.Web.Abstractions;
using Signaturit.Web.Areas.Catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signaturit.Web.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class TrialController : BaseController<TrialController>
    {
        public IActionResult Index()
        {
            var model = new TrialViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllTrialsCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<TrialViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                var trialViewModel = new TrialViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", trialViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetTrialByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var trialViewModel = _mapper.Map<TrialViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", trialViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, TrialViewModel trial)
        {
            bool backToList = false;

            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createTrialCommand = _mapper.Map<CreateTrialCommand>(trial);
                    var result = await _mediator.Send(createTrialCommand);
 
                    if (result.IsValidResponse)
                    {
                        id = result.Result.Data;
                        _notify.Success($"Trial with ID {id} Created.");
                        backToList = true;
                    }
                    else
                    {
                        string message = result.Errors.First();

                        message = string.Join(Environment.NewLine, result.Errors);

                        _notify.Error(message);

                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", trial);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }
                else
                {
                    var updateTrialCommand = _mapper.Map<UpdateTrialCommand>(trial);
                    var result = await _mediator.Send(updateTrialCommand);

                    if (result.IsValidResponse)
                    {
                        id = result.Result.Data;
                        _notify.Information($"Trial with ID {id} Updated.");
                        backToList = true;
                    }
                    else
                    {
                        string message = result.Errors.First();
                        message = string.Join(Environment.NewLine, result.Errors);

                        _notify.Error(message);

                        var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", trial);
                        return new JsonResult(new { isValid = false, html = html });
                    }
                }

                if (backToList)
                {
                    var response = await _mediator.Send(new GetAllTrialsCachedQuery());
                    if (response.Succeeded)
                    {
                        var viewModel = _mapper.Map<List<TrialViewModel>>(response.Data);
                        var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                        return new JsonResult(new { isValid = true, html = html });
                    }
                    else
                    {
                        _notify.Error(response.Message);
                        return null;
                    }
                }

                return null;
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", trial);

                // Client side validaciones

                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                string messages = string.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage + " " + v.Exception));

                _notify.Error(messages);

                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteTrialCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Trial with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllTrialsCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<TrialViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}