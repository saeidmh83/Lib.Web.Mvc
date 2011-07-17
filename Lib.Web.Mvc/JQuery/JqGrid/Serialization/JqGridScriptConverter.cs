﻿using System;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections;

namespace Lib.Web.Mvc.JQuery.JqGrid.Serialization
{
    internal class JqGridScriptConverter : JavaScriptConverter
    {
        #region Fields
        private static ReadOnlyCollection<Type> _supportedTypes = new ReadOnlyCollection<Type>(new List<Type>()
        {
            typeof(JqGridOptions),
            typeof(JqGridColumnModel),
            typeof(JqGridColumnEditOptions),
            typeof(JqGridColumnEditRules),
            typeof(JqGridColumnFormOptions),
            typeof(JqGridColumnFormatterOptions),
            typeof(JqGridColumnSearchOptions),
            typeof(JqGridSubgridModel),
            typeof(JqGridResponse),
            typeof(JqGridRecord),
            typeof(JqGridRequestSearchingFilters),
            typeof(JqGridRequestSearchingFilter)
        });
        #endregion

        #region JavaScriptConverter Members
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            object obj = null;

            if (dictionary != null)
            {
                if (type == typeof(JqGridOptions))
                    obj = DeserializeJqGridOptions(dictionary, serializer);
                else if (type == typeof(JqGridColumnModel))
                    obj = DeserializeJqGridColumnModel(dictionary, serializer);
                else if (type == typeof(JqGridColumnEditOptions))
                    obj = DeserializeJqGridColumnEditOptions(dictionary, serializer);
                else if (type == typeof(JqGridColumnEditRules))
                    obj = DeserializeJqGridColumnEditRules(dictionary, serializer);
                else if (type == typeof(JqGridColumnFormOptions))
                    obj = DeserializeJqGridColumnFormOptions(dictionary, serializer);
                else if (type == typeof(JqGridColumnFormatterOptions))
                    obj = DeserializeJqGridColumnFormatterOptions(dictionary, serializer);
                else if (type == typeof(JqGridColumnSearchOptions))
                    obj = DeserializeJqGridColumnSearchOptions(dictionary, serializer);
                else if (type == typeof(JqGridSubgridModel))
                    obj = DeserializeJqGridSubgridModel(dictionary, serializer);
                else if (type == typeof(JqGridRequestSearchingFilters))
                    obj = DeserializeJqGridRequestSearchingFilters(dictionary, serializer);
                else if (type == typeof(JqGridRequestSearchingFilter))
                    obj = DeserializeJqGridRequestSearchingFilter(dictionary, serializer);
            }

            return obj;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            Dictionary<string, object> serializedObj = new Dictionary<string, object>();

            if (obj != null)
            {
                if (obj is JqGridOptions)
                    SerializeJqGridOptions((JqGridOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnModel)
                    SerializeJqGridColumnModel((JqGridColumnModel)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnEditOptions)
                    SerializeJqGridColumnEditOptions((JqGridColumnEditOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnEditRules)
                    SerializeJqGridColumnEditRules((JqGridColumnEditRules)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnFormatterOptions)
                    SerializeJqGridColumnFormatterOptions((JqGridColumnFormatterOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnFormOptions)
                    SerializeJqGridColumnFormOptions((JqGridColumnFormOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridColumnSearchOptions)
                    SerializeJqGridColumnSearchOptions((JqGridColumnSearchOptions)obj, serializer, ref serializedObj);
                else if (obj is JqGridSubgridModel)
                    SerializeJqGridSubgridModel((JqGridSubgridModel)obj, serializer, ref serializedObj);
                else if (obj is JqGridResponse)
                    SerializeJqGridResponse((JqGridResponse)obj, serializer, ref serializedObj);
                else if (obj is JqGridRecord)
                    SerializeJqGridRecord((JqGridRecord)obj, serializer, ref serializedObj);
            }

            return serializedObj;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { return _supportedTypes; }
        }
        #endregion

        #region Methods
        private static JqGridOptions DeserializeJqGridOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            string id = GetStringFromSerializedObj(serializedObj, "id");
            if (!String.IsNullOrWhiteSpace(id))
            {
                JqGridOptions obj = new JqGridOptions(id);
                obj.CellEditingEnabled = GetBooleanFromSerializedObj(serializedObj, "cellEdit");
                obj.CellEditingSubmitMode = GetEnumFromSerializedObj<JqGridCellEditingSubmitModes>(serializedObj, "cellsubmit");
                obj.CellEditingUrl = GetStringFromSerializedObj(serializedObj, "cellurl");

                if (serializedObj.ContainsKey("colModel") && serializedObj["colModel"] is ArrayList)
                {
                    foreach (object innerSerializedObj in (ArrayList)serializedObj["colModel"])
                    {
                        if (innerSerializedObj is IDictionary<string, object>)
                        {
                            JqGridColumnModel columnModel = DeserializeJqGridColumnModel((IDictionary<string, object>)innerSerializedObj, serializer);
                            if (columnModel != null)
                                obj.ColumnsModels.Add(columnModel);
                        }
                    }
                }

                if (serializedObj.ContainsKey("colNames") && serializedObj["colNames"] is ArrayList)
                {
                    foreach (object innerSerializedObj in (ArrayList)serializedObj["colNames"])
                        obj.ColumnsNames.Add(innerSerializedObj.ToString());
                }

                obj.Caption = GetStringFromSerializedObj(serializedObj, "caption");
                obj.DataString = GetStringFromSerializedObj(serializedObj, "datastr");
                obj.DataType = GetEnumFromSerializedObj<JqGridDataTypes>(serializedObj, "datatype", obj.DataType);
                obj.EditingUrl = GetStringFromSerializedObj(serializedObj, "editurl");
                obj.ExpandColumnClick = GetBooleanFromSerializedObj(serializedObj, "ExpandColClick");
                obj.ExpandColumn = GetStringFromSerializedObj(serializedObj, "ExpandColumn");
                obj.Height = GetInt32FromSerializedObj(serializedObj, "height");
                obj.MethodType = GetEnumFromSerializedObj<JqGridMethodTypes>(serializedObj, "mtype", obj.MethodType);
                
                if (serializedObj.ContainsKey("pager") && serializedObj["pager"] != null)
                    obj.Pager = true;

                if (serializedObj.ContainsKey("remapColumns") && serializedObj["remapColumns"] is ArrayList)
                {
                    obj.ColumnsRemaping = new List<int>();
                    foreach (object innerSerializedObj in (ArrayList)serializedObj["remapColumns"])
                    {
                        if (innerSerializedObj is Int32)
                            obj.ColumnsRemaping.Add((int)innerSerializedObj);
                    }
                }

                obj.RowsNumber = GetInt32FromSerializedObj(serializedObj, "rowNum", obj.RowsNumber);
                obj.SortingName = GetStringFromSerializedObj(serializedObj, "sortname");
                obj.SortingOrder = GetEnumFromSerializedObj<JqGridSortingOrders>(serializedObj, "sortorder", obj.SortingOrder);
                obj.SubgridEnabled = GetBooleanFromSerializedObj(serializedObj, "subGrid");

                if (serializedObj.ContainsKey("subGridModel") && serializedObj["subGridModel"] != null && serializedObj["subGridModel"] is IDictionary<string, object>)
                    obj.SubgridModel = DeserializeJqGridSubgridModel((IDictionary<string, object>)serializedObj["subGridModel"], serializer);

                obj.SubgridUrl = GetStringFromSerializedObj(serializedObj, "subGridUrl");
                obj.SubgridColumnWidth = GetInt32FromSerializedObj(serializedObj, "subGridWidth");
                obj.TreeGridEnabled = GetBooleanFromSerializedObj(serializedObj, "treeGrid");
                obj.TreeGridModel = GetEnumFromSerializedObj<JqGridTreeGridModels>(serializedObj, "treeGridModel");
                obj.Url = GetStringFromSerializedObj(serializedObj, "url");
                obj.ViewRecords = GetBooleanFromSerializedObj(serializedObj, "viewrecords", obj.ViewRecords);
                obj.Width = GetInt32FromSerializedObj(serializedObj, "width");

                return obj;
            }
            else
                return null;
        }

        private static void SerializeJqGridOptions(JqGridOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("id", obj.Id);

            if (obj.CellEditingEnabled.HasValue)
            {
                serializedObj.Add("cellEdit", obj.CellEditingEnabled.Value);
                if (obj.CellEditingEnabled.Value)
                {
                    if (obj.CellEditingSubmitMode.HasValue)
                        serializedObj.Add("cellsubmit", obj.CellEditingSubmitMode.Value.ToString().ToLower());

                    if (!String.IsNullOrWhiteSpace(obj.CellEditingUrl))
                        serializedObj.Add("cellurl", obj.CellEditingUrl);
                }
            }

            serializedObj.Add("colModel", obj.ColumnsModels);
            serializedObj.Add("colNames", obj.ColumnsNames);

            if (!String.IsNullOrEmpty(obj.Caption))
                serializedObj.Add("caption", obj.Caption);

            serializedObj.Add("datatype", obj.DataType.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(obj.EditingUrl))
                serializedObj.Add("editurl", obj.EditingUrl);

            if (obj.ExpandColumnClick.HasValue)
                serializedObj.Add("ExpandColClick", obj.ExpandColumnClick.Value);

            if (!String.IsNullOrWhiteSpace(obj.ExpandColumn))
                serializedObj.Add("ExpandColumn", obj.ExpandColumn);

            if (obj.Height.HasValue)
                serializedObj.Add("height", obj.Height.Value);
            else
                serializedObj.Add("height", "100%");

            serializedObj.Add("mtype", obj.MethodType.ToString().ToUpper());

            if (obj.Pager)
                serializedObj.Add("pager", String.Format("#{0}Pager", obj.Id));

            serializedObj.Add("remapColumns", obj.ColumnsRemaping);
            serializedObj.Add("rowNum", obj.RowsNumber);
            serializedObj.Add("sortname", obj.SortingName);
            serializedObj.Add("sortorder", obj.SortingOrder.ToString().ToLower());

            if (obj.SubgridEnabled.HasValue)
            {
                serializedObj.Add("subGrid", obj.SubgridEnabled.Value);
                if (obj.SubgridEnabled.Value)
                {
                    if (obj.SubgridModel != null)
                        serializedObj.Add("subGridModel", obj.SubgridModel);

                    if (!String.IsNullOrWhiteSpace(obj.SubgridUrl))
                        serializedObj.Add("subGridUrl", obj.SubgridUrl);

                    if (obj.SubgridColumnWidth.HasValue)
                        serializedObj.Add("subGridWidth", obj.SubgridColumnWidth.Value);
                }
            }

            if (obj.TreeGridEnabled.HasValue)
                serializedObj.Add("treeGrid", obj.TreeGridEnabled.Value);

            if (obj.TreeGridModel.HasValue)
                serializedObj.Add("treeGridModel", obj.TreeGridModel.Value.ToString().ToLower());

            if (obj.UseDataString())
                serializedObj.Add("datastr", obj.DataString);
            else
                serializedObj.Add("url", obj.Url);

            serializedObj.Add("viewrecords", obj.ViewRecords);

            if (obj.Width.HasValue)
                serializedObj.Add("width", obj.Width.Value);
        }

        private static JqGridColumnModel DeserializeJqGridColumnModel(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            string name = GetStringFromSerializedObj(serializedObj, "name");
            if (!String.IsNullOrWhiteSpace(name))
            {
                JqGridColumnModel obj = new JqGridColumnModel(name);

                obj.Alignment = GetEnumFromSerializedObj<JqGridAlignments>(serializedObj, "align", obj.Alignment);
                obj.Classes = GetStringFromSerializedObj(serializedObj, "classes");
                obj.Editable = GetBooleanFromSerializedObj(serializedObj, "editable");
                obj.EditType = GetEnumFromSerializedObj<JqGridColumnEditTypes>(serializedObj, "edittype");

                if (serializedObj.ContainsKey("editoptions") && serializedObj["editoptions"] != null && serializedObj["editoptions"] is IDictionary<string, object>)
                    obj.EditOptions = DeserializeJqGridColumnEditOptions((IDictionary<string, object>)serializedObj["editoptions"], serializer);

                if (serializedObj.ContainsKey("editrules") && serializedObj["editrules"] != null && serializedObj["editrules"] is IDictionary<string, object>)
                    obj.EditRules = DeserializeJqGridColumnEditRules((IDictionary<string, object>)serializedObj["editrules"], serializer);

                obj.Fixed = GetBooleanFromSerializedObj(serializedObj, "fixed");
                obj.Hidden = GetBooleanFromSerializedObj(serializedObj, "hidden", false);

                if (serializedObj.ContainsKey("formatoptions") && serializedObj["formatoptions"] != null && serializedObj["formatoptions"] is IDictionary<string, object>)
                    obj.FormatterOptions = DeserializeJqGridColumnFormatterOptions((IDictionary<string, object>)serializedObj["formatoptions"], serializer);

                if (serializedObj.ContainsKey("formoptions") && serializedObj["formoptions"] != null && serializedObj["formoptions"] is IDictionary<string, object>)
                    obj.FormOptions = DeserializeJqGridColumnFormOptions((IDictionary<string, object>)serializedObj["formoptions"], serializer);

                obj.InitialSortingOrder = GetEnumFromSerializedObj<JqGridSortingOrders>(serializedObj, "firstsortorder");
                obj.Formatter = GetStringFromSerializedObj(serializedObj, "formatter");
                obj.Resizable = GetBooleanFromSerializedObj(serializedObj, "resizable");
                obj.Sortable = GetBooleanFromSerializedObj(serializedObj, "sortable");
                obj.Index = GetStringFromSerializedObj(serializedObj, "index");
                obj.Searchable = GetBooleanFromSerializedObj(serializedObj, "search");
                obj.SearchType = GetEnumFromSerializedObj<JqGridColumnSearchTypes>(serializedObj, "stype");

                if (serializedObj.ContainsKey("searchoptions") && serializedObj["searchoptions"] != null && serializedObj["searchoptions"] is IDictionary<string, object>)
                    obj.SearchOptions = DeserializeJqGridColumnSearchOptions((IDictionary<string, object>)serializedObj["searchoptions"], serializer);

                obj.UnFormatter = GetStringFromSerializedObj(serializedObj, "unformat");
                obj.Width = GetInt32FromSerializedObj(serializedObj, "width");

                return obj;
            }
            else
                return null;
        }

        private static void SerializeJqGridColumnModel(JqGridColumnModel obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("align", obj.Alignment.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(obj.Classes))
                serializedObj.Add("classes", obj.Classes);

            if (obj.Editable.HasValue)
            {
                serializedObj.Add("editable", obj.Editable.Value);
                if (obj.Editable.Value)
                {
                    if (obj.EditType.HasValue)
                        serializedObj.Add("edittype", obj.EditType.Value.ToString().ToLower());

                    if (obj.EditOptions != null)
                        serializedObj.Add("editoptions", obj.EditOptions);

                    if (obj.EditRules != null)
                        serializedObj.Add("editrules", obj.EditRules);

                    if (obj.FormOptions != null)
                        serializedObj.Add("formoptions", obj.FormOptions);
                }
            }

            if (obj.Fixed.HasValue)
                serializedObj.Add("fixed", obj.Fixed.Value);

            if (obj.FormatterOptions != null)
                serializedObj.Add("formatoptions", obj.FormatterOptions);

            if (obj.InitialSortingOrder.HasValue)
                serializedObj.Add("firstsortorder", obj.InitialSortingOrder.Value.ToString().ToLower());

            if (!String.IsNullOrWhiteSpace(obj.Formatter))
                serializedObj.Add("formatter", obj.Formatter);

            serializedObj.Add("hidden", obj.Hidden);

            if (obj.Resizable.HasValue)
                serializedObj.Add("resizable", obj.Resizable.Value);

            if (obj.Sortable.HasValue)
                serializedObj.Add("sortable", obj.Sortable.Value);

            serializedObj.Add("index", obj.Index);

            if (obj.Searchable.HasValue)
            {
                serializedObj.Add("search", obj.Searchable.Value);
                if (obj.Searchable.Value)
                {
                    if (obj.SearchType.HasValue)
                        serializedObj.Add("stype", obj.SearchType.Value.ToString().ToLower());

                    if (obj.SearchOptions != null)
                        serializedObj.Add("searchoptions", obj.SearchOptions);
                }
            }

            serializedObj.Add("name", obj.Name);

            if (!String.IsNullOrWhiteSpace(obj.UnFormatter))
                serializedObj.Add("unformat", obj.UnFormatter);

            if (obj.Width.HasValue)
                serializedObj.Add("width", obj.Width.Value);
        }

        private static JqGridColumnEditOptions DeserializeJqGridColumnEditOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnEditOptions obj = new JqGridColumnEditOptions();

            obj.CustomElementFunction = GetStringFromSerializedObj(serializedObj, "custom_element");
            obj.CustomValueFunction = GetStringFromSerializedObj(serializedObj, "custom_value");
            obj.DataUrl = GetStringFromSerializedObj(serializedObj, "dataUrl");
            obj.MaximumLength = GetInt32FromSerializedObj(serializedObj, "maxlength");
            obj.MultipleSelect = GetBooleanFromSerializedObj(serializedObj, "multiple");
            obj.Source = GetStringFromSerializedObj(serializedObj, "src");

            return obj;
        }

        private static void SerializeJqGridColumnEditOptions(JqGridColumnEditOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (!String.IsNullOrWhiteSpace(obj.CustomElementFunction))
                serializedObj.Add("custom_element", obj.CustomElementFunction);

            if (!String.IsNullOrWhiteSpace(obj.CustomValueFunction))
                serializedObj.Add("custom_value", obj.CustomValueFunction);

            if (!String.IsNullOrWhiteSpace(obj.DataUrl))
                serializedObj.Add("dataUrl", obj.DataUrl);

            if (obj.MaximumLength.HasValue)
                serializedObj.Add("maxlength", obj.MaximumLength.Value);

            if (obj.MultipleSelect.HasValue)
                serializedObj.Add("multiple", obj.MultipleSelect.Value);

            if (!String.IsNullOrWhiteSpace(obj.Source))
                serializedObj.Add("src", obj.Source);
        }

        private static JqGridColumnEditRules DeserializeJqGridColumnEditRules(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnEditRules obj = new JqGridColumnEditRules();

            obj.Custom = GetBooleanFromSerializedObj(serializedObj, "custom");
            obj.CustomFunction = GetStringFromSerializedObj(serializedObj, "custom_func");
            obj.Date = GetBooleanFromSerializedObj(serializedObj, "date");
            obj.EditHidden = GetBooleanFromSerializedObj(serializedObj, "edithidden");
            obj.Email = GetBooleanFromSerializedObj(serializedObj, "email");
            obj.Integer = GetBooleanFromSerializedObj(serializedObj, "integer");
            obj.MaxValue = GetDoubleFromSerializedObj(serializedObj, "maxValue");
            obj.MinValue = GetDoubleFromSerializedObj(serializedObj, "minValue");
            obj.Number = GetBooleanFromSerializedObj(serializedObj, "number");
            obj.Required = GetBooleanFromSerializedObj(serializedObj, "required");
            obj.Time = GetBooleanFromSerializedObj(serializedObj, "time");
            obj.Url = GetBooleanFromSerializedObj(serializedObj, "url");

            return obj;
        }

        private static void SerializeJqGridColumnEditRules(JqGridColumnEditRules obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (obj.Custom.HasValue)
                serializedObj.Add("custom", obj.Custom.Value);

            if (!String.IsNullOrWhiteSpace(obj.CustomFunction))
                serializedObj.Add("custom_func", obj.CustomFunction);

            if (obj.Date.HasValue)
                serializedObj.Add("date", obj.Date.Value);

            if (obj.EditHidden.HasValue)
                serializedObj.Add("edithidden", obj.EditHidden.Value);

            if (obj.Email.HasValue)
                serializedObj.Add("email", obj.Email.Value);

            if (obj.Integer.HasValue)
                serializedObj.Add("integer", obj.Integer.Value);

            if (obj.MaxValue.HasValue)
                serializedObj.Add("maxValue", obj.MaxValue.Value);

            if (obj.MinValue.HasValue)
                serializedObj.Add("minValue", obj.MinValue.Value);

            if (obj.Number.HasValue)
                serializedObj.Add("number", obj.Number.Value);

            if (obj.Required.HasValue)
                serializedObj.Add("required", obj.Required.Value);

            if (obj.Time.HasValue)
                serializedObj.Add("time", obj.Time.Value);

            if (obj.Url.HasValue)
                serializedObj.Add("url", obj.Url.Value);
        }

        private static JqGridColumnFormOptions DeserializeJqGridColumnFormOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnFormOptions obj = new JqGridColumnFormOptions();

            obj.ColumnPosition = GetInt32FromSerializedObj(serializedObj, "colpos");
            obj.ElementPrefix = GetStringFromSerializedObj(serializedObj, "elmprefix");
            obj.ElementSuffix = GetStringFromSerializedObj(serializedObj, "elmsuffix");
            obj.Label = GetStringFromSerializedObj(serializedObj, "label");
            obj.RowPosition = GetInt32FromSerializedObj(serializedObj, "rowpos");
            
            return obj;
        }

        private static void SerializeJqGridColumnFormOptions(JqGridColumnFormOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (obj.ColumnPosition.HasValue)
                serializedObj.Add("colpos", obj.ColumnPosition.Value);
            
            if (!String.IsNullOrWhiteSpace(obj.ElementPrefix))
                serializedObj.Add("elmprefix", obj.ElementPrefix);

            if (!String.IsNullOrWhiteSpace(obj.ElementSuffix))
                serializedObj.Add("elmsuffix", obj.ElementSuffix);

            if (!String.IsNullOrWhiteSpace(obj.Label))
                serializedObj.Add("label", obj.Label);

            if (obj.RowPosition.HasValue)
                serializedObj.Add("rowpos", obj.RowPosition.Value);
        }

        private static JqGridColumnFormatterOptions DeserializeJqGridColumnFormatterOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnFormatterOptions obj = new JqGridColumnFormatterOptions();

            obj.AddParam = GetStringFromSerializedObj(serializedObj, "addParam");
            obj.BaseLinkUrl = GetStringFromSerializedObj(serializedObj, "baseLinkUrl");
            obj.DecimalPlaces = GetInt32FromSerializedObj(serializedObj, "decimalPlaces");
            obj.DecimalSeparator = GetStringFromSerializedObj(serializedObj, "decimalSeparator");
            obj.DefaulValue = GetStringFromSerializedObj(serializedObj, "defaulValue");
            obj.Disabled = GetBooleanFromSerializedObj(serializedObj, "disabled");
            obj.IdName = GetStringFromSerializedObj(serializedObj, "idName");
            obj.Prefix = GetStringFromSerializedObj(serializedObj, "prefix");
            obj.ShowAction = GetStringFromSerializedObj(serializedObj, "showAction");
            obj.SourceFormat = GetStringFromSerializedObj(serializedObj, "srcformat");
            obj.Suffix = GetStringFromSerializedObj(serializedObj, "suffix");
            obj.Target = GetStringFromSerializedObj(serializedObj, "target");
            obj.TargetFormat = GetStringFromSerializedObj(serializedObj, "newformat");
            obj.ThousandsSeparator = GetStringFromSerializedObj(serializedObj, "thousandsSeparator");

            return obj;
        }

        private static void SerializeJqGridColumnFormatterOptions(JqGridColumnFormatterOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (!String.IsNullOrWhiteSpace(obj.AddParam))
                serializedObj.Add("addParam", obj.AddParam);

            if (!String.IsNullOrWhiteSpace(obj.BaseLinkUrl))
                serializedObj.Add("baseLinkUrl", obj.BaseLinkUrl);

            if (obj.DecimalPlaces.HasValue)
                serializedObj.Add("decimalPlaces", obj.DecimalPlaces);

            if (!String.IsNullOrWhiteSpace(obj.DecimalSeparator))
                serializedObj.Add("decimalSeparator", obj.DecimalSeparator);

            if (!String.IsNullOrWhiteSpace(obj.DefaulValue))
                serializedObj.Add("defaulValue", obj.DefaulValue);

            if (obj.Disabled.HasValue)
                serializedObj.Add("disabled", obj.Disabled);

            if (!String.IsNullOrWhiteSpace(obj.IdName))
                serializedObj.Add("idName", obj.IdName);

            if (!String.IsNullOrWhiteSpace(obj.Prefix))
                serializedObj.Add("prefix", obj.Prefix);

            if (!String.IsNullOrWhiteSpace(obj.ShowAction))
                serializedObj.Add("showAction", obj.ShowAction);

            if (!String.IsNullOrWhiteSpace(obj.SourceFormat))
                serializedObj.Add("srcformat", obj.SourceFormat);

            if (!String.IsNullOrWhiteSpace(obj.Suffix))
                serializedObj.Add("suffix", obj.Suffix);

            if (!String.IsNullOrWhiteSpace(obj.Target))
                serializedObj.Add("target", obj.Target);

            if (!String.IsNullOrWhiteSpace(obj.TargetFormat))
                serializedObj.Add("newformat", obj.TargetFormat);

            if (!String.IsNullOrWhiteSpace(obj.ThousandsSeparator))
                serializedObj.Add("thousandsSeparator", obj.ThousandsSeparator);            
        }

        private static JqGridColumnSearchOptions DeserializeJqGridColumnSearchOptions(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridColumnSearchOptions obj = new JqGridColumnSearchOptions();

            obj.DataUrl = GetStringFromSerializedObj(serializedObj, "dataUrl");
            obj.DefaultValue = GetStringFromSerializedObj(serializedObj, "defaultValue");
            obj.SearchHidden = GetBooleanFromSerializedObj(serializedObj, "searchhidden");

            if (serializedObj.ContainsKey("sopt") && serializedObj["sopt"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["sopt"])
                {
                    JqGridSearchOperators searchOperator = JqGridSearchOperators.Eq;
                    if (Enum.TryParse<JqGridSearchOperators>(innerSerializedObj.ToString(), true, out searchOperator))
                    {
                        if (obj.SearchOperators.HasValue)
                            obj.SearchOperators = obj.SearchOperators | searchOperator;
                        else
                            obj.SearchOperators = searchOperator;
                    }
                }
            }
            
            return obj;
        }

        private static void SerializeJqGridColumnSearchOptions(JqGridColumnSearchOptions obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            if (!String.IsNullOrWhiteSpace(obj.DataUrl))
                serializedObj.Add("dataUrl", obj.DataUrl);

            if (!String.IsNullOrWhiteSpace(obj.DefaultValue))
                serializedObj.Add("defaultValue", obj.DefaultValue);

            if (obj.SearchHidden.HasValue)
                serializedObj.Add("searchhidden", obj.SearchHidden.Value);

            if (obj.SearchOperators.HasValue)
            {
                List<string> searchOperators = new List<string>();
                foreach (JqGridSearchOperators searchOperator in Enum.GetValues(typeof(JqGridSearchOperators)))
                {
                    if ((obj.SearchOperators & searchOperator) == searchOperator)
                        searchOperators.Add(Enum.GetName(typeof(JqGridSearchOperators), searchOperator).ToLower());
                }
                serializedObj.Add("sopt", searchOperators);
            }
        }

        private static JqGridSubgridModel DeserializeJqGridSubgridModel(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridSubgridModel obj = new JqGridSubgridModel();

            if (serializedObj.ContainsKey("name") && serializedObj["name"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["name"])
                    obj.ColumnsNames.Add(innerSerializedObj.ToString());
            }

            if (serializedObj.ContainsKey("align") && serializedObj["align"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["align"])
                {
                    JqGridAlignments alignment = JqGridAlignments.Left;
                    Enum.TryParse<JqGridAlignments>(innerSerializedObj.ToString(), true, out alignment);
                    obj.ColumnsAlignments.Add(alignment);
                }
            }

            if (serializedObj.ContainsKey("width") && serializedObj["width"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["width"])
                {
                    if (innerSerializedObj is Int32)
                        obj.ColumnsWidths.Add((int)innerSerializedObj);
                }
            }

            return obj;
        }

        private static void SerializeJqGridSubgridModel(JqGridSubgridModel obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("name", obj.ColumnsNames);

            List<string> alignments = new List<string>();
            foreach (JqGridAlignments alignment in obj.ColumnsAlignments)
                alignments.Add(alignment.ToString().ToLower());
            serializedObj.Add("align", alignments);

            serializedObj.Add("width", obj.ColumnsWidths);
        }

        private static void SerializeJqGridResponse(JqGridResponse obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            serializedObj.Add("page", obj.PageIndex + 1);
            serializedObj.Add("records", obj.TotalRecordsCount);
            serializedObj.Add("rows", obj.Records);
            serializedObj.Add("total", obj.TotalPagesCount);
        }

        private static void SerializeJqGridRecord(JqGridRecord obj, JavaScriptSerializer serializer, ref Dictionary<string, object> serializedObj)
        {
            List<object> values = obj.Values;

            JqGridAdjacencyTreeRecord adjacencyTreeRecord = obj as JqGridAdjacencyTreeRecord;
            if (adjacencyTreeRecord != null)
            {
                values.Add(adjacencyTreeRecord.Level);
                values.Add(adjacencyTreeRecord.ParentId);
                values.Add(adjacencyTreeRecord.Leaf);
                values.Add(adjacencyTreeRecord.Expanded);
            }
            else
            {
                JqGridNestedSetTreeRecord nestedSetTreeRecord = obj as JqGridNestedSetTreeRecord;
                if (nestedSetTreeRecord != null)
                {
                    values.Add(nestedSetTreeRecord.Level);
                    values.Add(nestedSetTreeRecord.LeftField);
                    values.Add(nestedSetTreeRecord.RightField);
                    values.Add(nestedSetTreeRecord.Leaf);
                    values.Add(nestedSetTreeRecord.Expanded);
                }
            }

            serializedObj.Add("cell", values);
            serializedObj.Add("id", obj.Id);
        }

        private static JqGridRequestSearchingFilters DeserializeJqGridRequestSearchingFilters(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridRequestSearchingFilters obj = new JqGridRequestSearchingFilters();

            obj.GroupingOperator = GetEnumFromSerializedObj<JqGridSearchGroupingOperators>(serializedObj, "groupOp", JqGridSearchGroupingOperators.And);
            if (serializedObj.ContainsKey("rules") && serializedObj["rules"] is ArrayList)
            {
                foreach (object innerSerializedObj in (ArrayList)serializedObj["rules"])
                {
                    if (innerSerializedObj is IDictionary<string, object>)
                    {
                        JqGridRequestSearchingFilter searchingFilter = DeserializeJqGridRequestSearchingFilter((IDictionary<string, object>)innerSerializedObj, serializer);
                        obj.Filters.Add(searchingFilter);
                    }
                }
            }

            return obj;
        }

        private static JqGridRequestSearchingFilter DeserializeJqGridRequestSearchingFilter(IDictionary<string, object> serializedObj, JavaScriptSerializer serializer)
        {
            JqGridRequestSearchingFilter obj = new JqGridRequestSearchingFilter();

            obj.SearchingName = GetStringFromSerializedObj(serializedObj, "field");
            obj.SearchingOperator = GetEnumFromSerializedObj<JqGridSearchOperators>(serializedObj, "op", JqGridSearchOperators.Eq);
            obj.SearchingValue = GetStringFromSerializedObj(serializedObj, "data");
            return obj;
        }

        private static bool? GetBooleanFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is Boolean)
                return (bool)serializedObj[key];
            else
                return null;
        }

        private static bool GetBooleanFromSerializedObj(IDictionary<string, object> serializedObj, string key, bool defaultValue)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is Boolean)
                return (bool)serializedObj[key];
            else
                return defaultValue;
        }

        private static double? GetDoubleFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            double value;
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && Double.TryParse(serializedObj[key].ToString(), out value))
                return value;
            else
                return null;
        }

        private static TEnum? GetEnumFromSerializedObj<TEnum>(IDictionary<string, object> serializedObj, string key) where TEnum : struct
        {
            TEnum value;
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && Enum.TryParse<TEnum>(serializedObj[key].ToString(), true, out value))
                return value;
            else
                return null;
        }

        private static TEnum GetEnumFromSerializedObj<TEnum>(IDictionary<string, object> serializedObj, string key, TEnum defaultValue) where TEnum : struct
        {
            TEnum value = defaultValue;
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && Enum.TryParse<TEnum>(serializedObj[key].ToString(), true, out value))
                return value;
            else
                return defaultValue;
        }

        private static int? GetInt32FromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is Int32)
                return (int)serializedObj[key];
            else
                return null;
        }

        private static int GetInt32FromSerializedObj(IDictionary<string, object> serializedObj, string key, int defaultValue)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null && serializedObj[key] is Int32)
                return (int)serializedObj[key];
            else
                return defaultValue;
        }

        private static string GetStringFromSerializedObj(IDictionary<string, object> serializedObj, string key)
        {
            if (serializedObj.ContainsKey(key) && serializedObj[key] != null)
                return serializedObj[key].ToString();
            else
                return null;
        }
        #endregion
    }
}
