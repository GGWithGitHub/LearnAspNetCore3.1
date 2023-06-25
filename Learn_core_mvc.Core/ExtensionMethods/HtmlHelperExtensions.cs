using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace Learn_core_mvc.Core.ExtensionMethods
{
    // I have installed below packages :-
    // Microsoft.AspNetCore.Html.Abstractions
    // Microsoft.AspNetCore.Mvc.ViewFeatures
    public static class HtmlHelperExtensions
    {
        //public static string GetString(this IHtmlContent content)
        //{
        //    using (var writer = new System.IO.StringWriter())
        //    {
        //        content.WriteTo(writer, HtmlEncoder.Default);
        //        return writer.ToString();
        //    }
        //}
        //public static IHtmlContent CheckBoxesFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression) where TProperty : IList where TModel : new()
        //{
        //    var enumType = (typeof(TProperty)).GetProperties()[2].PropertyType;

        //    if (!enumType.IsEnum)
        //        throw new ArgumentException("TProperty must be a list of enumerated types");

        //    TModel model = (TModel)html.ViewContext.ViewData.Model;

        //    if (model == null)
        //        html.ViewContext.ViewData.Model = new TModel();

        //    TProperty value = expression.Compile()(((TModel)html.ViewContext.ViewData.Model));

        //    var items = Enum.GetValues(enumType);
        //    Type genericClass = typeof(Enums<>);
        //    Type constructedClass = genericClass.MakeGenericType(enumType);

        //    dynamic created = Activator.CreateInstance(constructedClass);

        //    var methodInfo = constructedClass.GetMethod("GetDisplayValue");

        //    var selectItems = new List<SelectListItem>();

        //    foreach (var item in items)
        //    {
        //        var displayValue = methodInfo.Invoke(created, new[] { item });

        //        selectItems.Add(new SelectListItem
        //        {
        //            Text = displayValue.ToString(),
        //            Value = item.ToString(),
        //            Selected = value != null && ((IList)value).Contains(item)
        //        });
        //    }

        //    var name = html.ExpressionProvider().GetExpressionText(expression);

        //    var sb = new StringBuilder();
        //    var ul = new TagBuilder("ul");

        //    foreach (var item in selectItems)
        //    {
        //        var id = string.Format("{0}_{1}", name, item.Value);

        //        var li = new TagBuilder("li");
        //        li.Attributes.Add("class", "form-group checkbox");

        //        var checkBox = new TagBuilder("input");
        //        checkBox.Attributes.Add("id", id);
        //        checkBox.Attributes.Add("value", item.Value);
        //        checkBox.Attributes.Add("name", name);
        //        checkBox.Attributes.Add("type", "checkbox");

        //        if (item.Selected)
        //            checkBox.Attributes.Add("checked", "checked");

        //        var label = new TagBuilder("label");
        //        var outerLabel = new TagBuilder("label");

        //        label.Attributes.Add("for", id);
        //        outerLabel.Attributes.Add("for", id);

        //        outerLabel.InnerHtml.SetHtmlContent(checkBox.GetString() + "\r\n" + label.GetString() + "\r\n" + item.Text);

        //        li.InnerHtml.SetHtmlContent(outerLabel);

        //        sb.AppendLine(li.GetString());
        //    }

        //    ul.InnerHtml.SetHtmlContent(sb.ToString());

        //    return ul;
        //}
        //public static ModelExpressionProvider ExpressionProvider<TModel>(this IHtmlHelper<TModel> htmlHelper)
        //{
        //    return htmlHelper.ViewContext.HttpContext.RequestServices
        //        .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
        //}

        //public static IHtmlContent CheckBoxForBool<TModel>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression) where TModel : new()
        //{
        //    if (htmlHelper.ViewContext.ViewData.Model == null)
        //        htmlHelper.ViewContext.ViewData.Model = new TModel();

        //    bool value = expression.Compile()(((TModel)htmlHelper.ViewContext.ViewData.Model));

        //    var name = htmlHelper.ExpressionProvider().GetExpressionText(expression);

        //    var id = name;

        //    var metadata = htmlHelper.ExpressionProvider().CreateModelExpression(htmlHelper.ViewData, expression).Metadata;
        //    var resolvedLabelText = metadata.DisplayName ?? metadata.PropertyName ?? name.Split('.').Last();

        //    var div = new TagBuilder("div");

        //    div.Attributes.Add("class", "form-group checkbox");

        //    var checkBox = new TagBuilder("input");

        //    checkBox.Attributes.Add("id", id);
        //    checkBox.Attributes.Add("name", name);
        //    checkBox.Attributes.Add("type", "checkbox");
        //    checkBox.Attributes.Add("value", "true");

        //    var hiddenBox = new TagBuilder("input");

        //    hiddenBox.Attributes.Add("name", name);
        //    hiddenBox.Attributes.Add("type", "hidden");
        //    hiddenBox.Attributes.Add("value", "false");

        //    if (value)
        //        checkBox.Attributes.Add("selected", "true");

        //    var label = new TagBuilder("label");
        //    var outerLabel = new TagBuilder("label");

        //    label.Attributes.Add("for", id);
        //    outerLabel.Attributes.Add("for", id);

        //    outerLabel.InnerHtml.SetHtmlContent(checkBox.GetString() + "\r\n" + label.GetString() + "\r\n" + resolvedLabelText + "\r\n" + hiddenBox.GetString());

        //    div.InnerHtml.SetHtmlContent(outerLabel);

        //    return div;
        //}

        //public static IHtmlContent RadioButtonsFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression) where TProperty : IConvertible
        //{
        //    var Html = String.Empty;

        //    foreach (TProperty val in Enum.GetValues(typeof(TProperty)))
        //    {
        //        var displayValue = new Enums<TProperty>().GetDisplayValue(val);

        //        TagBuilder radioHtml = (TagBuilder)htmlHelper.RadioButtonFor(expression, val);

        //        //var name = TagBuilder.CreateSanitizedId( radioHtml.Attributes["name"], "_");
        //        var id = TagBuilder.CreateSanitizedId($"{radioHtml.Attributes["name"]}_{val}", "_");

        //        var labelHtml = htmlHelper.Label(null, displayValue, new { @for = id });

        //        radioHtml.Attributes["id"] = id;
        //        //radioHtml.Attributes["name"] = name;

        //        Html += "<div class=\"radio\">";
        //        Html += radioHtml.GetString();
        //        Html += labelHtml.GetString();
        //        Html += "</div>";
        //    }

        //    return new HtmlString(Html);
        //}
    }
}
