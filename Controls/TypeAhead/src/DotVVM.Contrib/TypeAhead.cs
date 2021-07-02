﻿using System;
using System.Collections.Generic;
using System.Linq;
using DotVVM.Framework.Binding.Properties;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Binding.Expressions;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a TypeAhead input field.
    /// </summary>
    [ControlMarkupOptions(AllowContent = false)]
    public class TypeAhead : Selector
    {

        public TypeAhead() : base("input")
        {
        }

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.AddAttribute("type", "text");

            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-DataSource", this, DataSourceProperty);
            writer.AddKnockoutDataBind("dotvvm-contrib-TypeAhead-SelectedValue", this, SelectedValueProperty);
            if (ItemValueBinding is IValueBinding itemValue)
            {
                writer.AddKnockoutDataBind(
                    "dotvvm-contrib-TypeAhead-ItemValue",
                    itemValue.GetProperty<SelectorItemBindingProperty>().Expression,
                    this
                );
            }

            if (ItemTextBinding is IValueBinding itemText)
            {
                writer.AddKnockoutDataBind(
                    "dotvvm-contrib-TypeAhead-ItemText",
                    itemText.GetProperty<SelectorItemBindingProperty>().Expression,
                    this
                );
            }

            var selectionChangedBinding = GetCommandBinding(SelectionChangedProperty);
            if (selectionChangedBinding != null)
            {
                writer.AddAttribute("onchange", KnockoutHelper.GenerateClientPostBackScript(nameof(SelectionChanged), selectionChangedBinding, this, isOnChange: true, useWindowSetTimeout: true));
            }

            base.AddAttributesToRender(writer, context);
        }

        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.TypeAhead");

            base.OnPreRender(context);
        }

        protected override void RenderBeginTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            writer.RenderSelfClosingTag(TagName);
        }

        protected override void RenderEndTag(IHtmlWriter writer, IDotvvmRequestContext context)
        {
        }
    }
}
