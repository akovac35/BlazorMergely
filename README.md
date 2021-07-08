# BlazorMergely

An implementation of Blazor diff and merge component based on Mergely with server side support.

![this](Resources/.NET_Core_Logo_small.png)

* [com.github.akovac35.BlazorMergely](https://www.nuget.org/packages/com.github.akovac35.BlazorMergely/)

	[![NuGet Version](http://img.shields.io/nuget/v/com.github.akovac35.BlazorMergely.svg?style=flat)](https://www.nuget.org/packages/com.github.akovac35.BlazorMergely/)

Browser functionality:

![this](Resources/simple-showcase.gif)

Interaction with the server:

![this](Resources/save-to-server-console.gif)

## Status

PRODUCTION READY starting from version 1.0.0.

## Samples

Review the following samples:

* [BlazorMergelySimpleTest](instance/BlazorMergelyWebApp/Pages/BlazorMergelySimpleTest.razor)
* [BlazorMergelyTest](instance/BlazorMergelyWebApp/Pages/BlazorMergelyTest.razor)

## Usage

Update _Host.cshtml file with static file references as follows:

```cshtml
@* BlazorMergely CSS references *@
<component type="typeof(BlazorMergelyCssReferences)" render-mode="Static"/>

@* BlazorMergely JS references *@
<component type="typeof(BlazorMergelyJsReferences)" render-mode="Static"/>
```

The above may not meet some specific criteria like jQuery version etc. In that case simply add the required static file references manually.

Then use the component as follows:

```razor
<div class="form-row">
	<div class="col-3">
		<button @onclick=@PrintContentsToConsoleOnServerSide>Print contents to console on server side</button>
	</div>
</div>

<hr />
<br />

<div class="form-row">
	<div class="col" style="height:400px">
		<BlazorMergelyComponent @ref="BlazorMergelyComponentInstance" MergelyOptions=@MergelyOptions />
	</div>
</div>

@code{
	private BlazorMergelyComponent? BlazorMergelyComponentInstance { get; set; }
	private string MergelyOptions { get; set; } = "{\"cmsettings\":{\"mode\":\"text/javascript\",\"readOnly\":false,\"styleSelectedText\": true}}"; // This is optional
	
	private TestModel TestModelInstance { get; set; } = new() { LeftValue = "var x = 0;", RightValue = "var y = 0;" };

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);
		
		// Component reference is set after the first render
		if(firstRender)
		{
			await BlazorMergelyComponentInstance!.SetValueAsync(TestModelInstance.LeftValue, TestModelInstance.RightValue);
		}
	}

	private async Task PrintContentsToConsoleOnServerSide(MouseEventArgs args)
	{
		var contents = await BlazorMergelyComponentInstance!.GetValuesAsync();
		Console.WriteLine(contents.Left);
		Console.WriteLine(contents.Right);
	}
}
```

## Release history

* 1.0.0 - Production ready.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## Credits

Based on Mergely javascript library: [www.mergely.com](https://mergely.com/)

## License
[LGPL-2.1](LICENSE)