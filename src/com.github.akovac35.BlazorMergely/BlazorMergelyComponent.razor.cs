using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace com.github.akovac35.BlazorMergely
{
    public partial class BlazorMergelyComponent : ComponentBase
    {
        [Inject]
        protected IJSRuntime IJSRuntimeInstance { get; private set; } = null!;

        /// <summary>
        /// Use this property for Mergely initialization. E.g. "{\"cmsettings\":{\"mode\":\"text/javascript\",\"readOnly\":false,\"styleSelectedText\": true}}"
        /// Changing this property after the first render will have no effect.
        /// </summary>
        [Parameter]
        public string? MergelyOptions { get; set; }

        /// <summary>
        /// Component title.
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        [Parameter]
        public string HtmlId { get; set; } = Guid.NewGuid().ToString();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await Init();
            }
        }

        protected virtual ValueTask Init()
        {
            return IJSRuntimeInstance.InvokeVoidAsync("BlazorMergely.init", $"#{HtmlId}", MergelyOptions!);
        }

        /// <summary>
        /// Changes the Code Mirror language display mode.
        /// </summary>
        /// <param name="modeStr">E.g. text/javascript, js, javascript</param>
        public ValueTask ChangeLangDisplayModeAsync(string? modeStr)
        {
            return IJSRuntimeInstance.InvokeVoidAsync("BlazorMergely.changeLangDisplayMode", $"#{HtmlId}", modeStr!);
        }

        /// <summary>
        /// Gets Mergely options.
        /// </summary>
        public ValueTask<string> GetOptionsAsync()
        {
            return IJSRuntimeInstance.InvokeAsync<string>("BlazorMergely.getOptions", $"#{HtmlId}");
        }

        /// <summary>
        /// Sets Mergely options.
        /// </summary>
        public ValueTask SetOptionsAsync(string optionsStr)
        {
            if (optionsStr is null)
            {
                throw new ArgumentNullException(nameof(optionsStr));
            }

            return IJSRuntimeInstance.InvokeVoidAsync("BlazorMergely.setOptions", $"#{HtmlId}", optionsStr);
        }

        /// <summary>
        /// Clears value.
        /// </summary>
        public ValueTask ClearAsync()
        {
            return IJSRuntimeInstance.InvokeVoidAsync("BlazorMergely.clear", $"#{HtmlId}");
        }

        /// <summary>
        /// Gets value.
        /// </summary>
        public ValueTask<string?> GetValueAsync(MergelySide side)
        {
            return IJSRuntimeInstance.InvokeAsync<string?>("BlazorMergely.get", $"#{HtmlId}", (side == MergelySide.LEFT ? "lhs" : "rhs"));
        }

        /// <summary>
        /// Gets left and right values.
        /// </summary>
        public async ValueTask<(string? Left, string? Right)> GetValuesAsync()
        {
            var arr = await IJSRuntimeInstance.InvokeAsync<string?[]>("BlazorMergely.getBoth", $"#{HtmlId}");

            return (arr[0], arr[1]);
        }

        /// <summary>
        /// Sets value.
        /// </summary>
        public ValueTask SetValueAsync(string? value, MergelySide side)
        {
            return IJSRuntimeInstance.InvokeVoidAsync($"BlazorMergely.{(side == MergelySide.LEFT ? "lhs" : "rhs")}", $"#{HtmlId}", value ?? "");

        }

        /// <summary>
        /// Sets value.
        /// </summary>
        public ValueTask SetValueAsync(string? lhsValue, string? rhsValue)
        {
            return IJSRuntimeInstance.InvokeVoidAsync($"BlazorMergely.set", $"#{HtmlId}", lhsValue ?? "", rhsValue ?? "");

        }

        /// <summary>
        /// Triggers Mergely update. This method does not need to be invoked if the Mergely autoupdate property is set to true.
        /// </summary>
        public ValueTask UpdateAsync()
        {
            return IJSRuntimeInstance.InvokeVoidAsync("BlazorMergely.update", $"#{HtmlId}");
        }

        public enum MergelySide
        {
            LEFT = 0,
            RIGHT = 1
        }
    }
}