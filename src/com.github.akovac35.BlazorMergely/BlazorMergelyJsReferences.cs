using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace com.github.akovac35.BlazorMergely
{
    public class BlazorMergelyJsReferences : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "script");
            builder.AddAttribute(1, "src", "_content/com.github.akovac35.BlazorMergely/jquery3.2.1.min.js");
            builder.CloseElement();

            builder.OpenElement(2, "script");
            builder.AddAttribute(3, "src", "_content/com.github.akovac35.BlazorMergely/codemirror/lib/codemirror.js");
            builder.CloseElement();

            builder.OpenElement(4, "script");
            builder.AddAttribute(5, "src", "_content/com.github.akovac35.BlazorMergely/codemirror/addon/mode/loadmode.js");
            builder.CloseElement();

            builder.OpenElement(6, "script");
            builder.AddAttribute(7, "src", "_content/com.github.akovac35.BlazorMergely/codemirror/mode/meta.js");
            builder.CloseElement();

            builder.OpenElement(8, "script");
            builder.AddAttribute(9, "src", "_content/com.github.akovac35.BlazorMergely/codemirror/addon/search/searchcursor.js");
            builder.CloseElement();

            builder.OpenElement(10, "script");
            builder.AddAttribute(11, "src", "_content/com.github.akovac35.BlazorMergely/mergely/mergely.js");
            builder.CloseElement();

            builder.OpenElement(12, "script");
            builder.AddAttribute(13, "src", "_content/com.github.akovac35.BlazorMergely/blazormergely.js");
            builder.CloseElement();

            base.BuildRenderTree(builder);
        }
    }
}