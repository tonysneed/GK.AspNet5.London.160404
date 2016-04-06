using Microsoft.AspNet.Razor.TagHelpers;

namespace HelloTagHelpers.TagHelpers
{
    public class SpanTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            var emoji = context.AllAttributes["emoji"];
            if (emoji != null && emoji.Value.ToString() == "smile")
            {
                output.Content.SetContent(" :) ");
                output.TagMode = TagMode.StartTagAndEndTag;
            }
        }
    }
}
