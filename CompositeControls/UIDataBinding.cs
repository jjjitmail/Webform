using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Reflection;

namespace UI
{
    public static class UIDataBinding
    {
        public static void InitLabelCheckBoxes(Control _Controls, bool value)
        {
            foreach (Control _SubControl in _Controls.Controls)
            {
                if (_SubControl is LabelCheckBox)
                {
                    LabelCheckBox _LabelCheckBox = (LabelCheckBox)_SubControl;
                    _LabelCheckBox.ToggleCheckBox(value);
                }
            }
        }

        public static void InitControlPermission<T>(ControlCollection _col, T _obj, RechtenLoopUpList<RechtenLoopUp> _RechtenLoopUpList, List<string> _userRoles)
        {
            foreach (Control _Control in _col)
            {
                InitThisPermission(_Control, _obj, _RechtenLoopUpList, _userRoles);
                foreach (Control _SubControl in _Control.Controls)
                {
                    InitThisPermission(_SubControl, _obj, _RechtenLoopUpList, _userRoles);
                }
            }
        }

        /// <summary>
        /// hier wordt de rechten aan betreffende controls bepaald
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="_Control">deze control</param>
        /// <param name="_obj">object</param>
        /// <param name="_RechtenLoopUpList">rechten lijst van _obj</param>
        private static void InitThisPermission<T>(Control _Control, T _obj, RechtenLoopUpList<RechtenLoopUp> _RechtenLoopUpList, List<string> _userRoles)
        {
            Type _Type = typeof(T);
            
            try
            {                
                if (_Control is LabelTekstVeld)
                {
                    LabelTekstVeld _LabelTekstVeld = (LabelTekstVeld)_Control;

                    PropertyInfo prop = _Type.GetProperty(_LabelTekstVeld.DataBinding);
                    Attribute[] attributes = (Attribute[])prop.GetCustomAttributes(typeof(Attribute), true);

                    Attribute attribute = attributes.Where(x => x.GetType().Name.Equals("RECHTEN_LOOKUP")).FirstOrDefault();

                    if (attribute != null)
                    {
                        sbyte TableId = (sbyte)attribute.GetType().GetProperty("TableId").GetValue(attribute, null);
                        sbyte Value = (sbyte)attribute.GetType().GetProperty("Value").GetValue(attribute, null);

                        // rechten van betreffende control ophalen
                        RechtenLoopUp _RechtenLoopUp = _RechtenLoopUpList.Where(x => x.TableId == TableId && x.Value == Value).FirstOrDefault();

                        // grootste getal (recht) filtreren
                        var max = _RechtenLoopUp.GetType().GetProperties()
                            .Where(x => _userRoles.Contains(x.Name))
                            .OrderByDescending(a => a.GetValue(_RechtenLoopUp, null))
                            .FirstOrDefault().GetValue(_RechtenLoopUp, null);

                        // init rechten voor deze control
                        _LabelTekstVeld.InitEigenschappen(Convert.ToSByte(max));
                    }
                    return;
                }
                if (_Control is ImageUploadVeld)
                {
                    ImageUploadVeld _ImageUploadVeld = (ImageUploadVeld)_Control;

                    PropertyInfo prop = _Type.GetProperty(_ImageUploadVeld.DataBinding);
                    Attribute[] attributes = (Attribute[])prop.GetCustomAttributes(typeof(Attribute), true);

                    Attribute attribute = attributes.Where(x => x.GetType().Name.Equals("RECHTEN_LOOKUP")).FirstOrDefault();

                    if (attribute != null)
                    {
                        sbyte TableId = (sbyte)attribute.GetType().GetProperty("TableId").GetValue(attribute, null);
                        sbyte Value = (sbyte)attribute.GetType().GetProperty("Value").GetValue(attribute, null);

                        // rechten van betreffende control ophalen
                        RechtenLoopUp _RechtenLoopUp = _RechtenLoopUpList.Where(x => x.TableId == TableId && x.Value == Value).FirstOrDefault();

                        // grootste getal (recht) filtreren
                        var max = _RechtenLoopUp.GetType().GetProperties()
                            .Where(x => _userRoles.Contains(x.Name))
                            .OrderByDescending(a => a.GetValue(_RechtenLoopUp, null))
                            .FirstOrDefault().GetValue(_RechtenLoopUp, null);

                        // init rechten voor deze control
                        _ImageUploadVeld.InitEigenschappen(Convert.ToSByte(max));
                    }
                    return;
                }
                if (_Control is PrisFileUpload)
                {
                    PrisFileUpload _FileUpload = (PrisFileUpload)_Control;

                    PropertyInfo prop = _Type.GetProperty(_FileUpload.DataBinding);
                    Attribute[] attributes = (Attribute[])prop.GetCustomAttributes(typeof(Attribute), true);

                    Attribute attribute = attributes.Where(x => x.GetType().Name.Equals("RECHTEN_LOOKUP")).FirstOrDefault();

                    if (attribute != null)
                    {
                        sbyte TableId = (sbyte)attribute.GetType().GetProperty("TableId").GetValue(attribute, null);
                        sbyte Value = (sbyte)attribute.GetType().GetProperty("Value").GetValue(attribute, null);

                        // rechten van betreffende control ophalen
                        RechtenLoopUp _RechtenLoopUp = _RechtenLoopUpList.Where(x => x.TableId == TableId && x.Value == Value).FirstOrDefault();

                        // grootste getal (recht) filtreren
                        var max = _RechtenLoopUp.GetType().GetProperties()
                            .Where(x => _userRoles.Contains(x.Name))
                            .OrderByDescending(a => a.GetValue(_RechtenLoopUp, null))
                            .FirstOrDefault().GetValue(_RechtenLoopUp, null);

                        // init rechten voor deze control
                        _FileUpload.InitEigenschappen(Convert.ToSByte(max));
                    }
                    return;
                }
                if (_Control is LabelCheckBox)
                {
                    LabelCheckBox _LabelCheckBox = (LabelCheckBox)_Control;

                    PropertyInfo prop = _Type.GetProperty(_LabelCheckBox.DataBinding);
                    Attribute[] attributes = (Attribute[])prop.GetCustomAttributes(typeof(Attribute), true);

                    Attribute attribute = attributes.Where(x => x.GetType().Name.Equals("RECHTEN_LOOKUP")).FirstOrDefault();

                    if (attribute != null)
                    {
                        sbyte TableId = (sbyte)attribute.GetType().GetProperty("TableId").GetValue(attribute, null);
                        sbyte Value = (sbyte)attribute.GetType().GetProperty("Value").GetValue(attribute, null);

                        // rechten van betreffende control ophalen
                        RechtenLoopUp _RechtenLoopUp = _RechtenLoopUpList.Where(x => x.TableId == TableId && x.Value == Value).FirstOrDefault();

                        // grootste getal (recht) filtreren
                        var max = _RechtenLoopUp.GetType().GetProperties()
                            .Where(x => _userRoles.Contains(x.Name))
                            .OrderByDescending(a => a.GetValue(_RechtenLoopUp, null))
                            .FirstOrDefault().GetValue(_RechtenLoopUp, null);

                        // init rechten voor deze control
                        _LabelCheckBox.InitEigenschappen(Convert.ToSByte(max));
                    }
                    return;
                }
                if (_Control is ComboBox)
                {
                    ComboBox _ComboBox = (ComboBox)_Control;

                    PropertyInfo prop = _Type.GetProperty(_ComboBox.DataBinding);
                    Attribute[] attributes = (Attribute[])prop.GetCustomAttributes(typeof(Attribute), true);

                    Attribute attribute = attributes.Where(x => x.GetType().Name.Equals("RECHTEN_LOOKUP")).FirstOrDefault();

                    if (attribute != null)
                    {
                        sbyte TableId = (sbyte)attribute.GetType().GetProperty("TableId").GetValue(attribute, null);
                        sbyte Value = (sbyte)attribute.GetType().GetProperty("Value").GetValue(attribute, null);

                        // rechten van betreffende control ophalen
                        RechtenLoopUp _RechtenLoopUp = _RechtenLoopUpList.Where(x => x.TableId == TableId && x.Value == Value).FirstOrDefault();

                        // grootste getal (recht) filtreren
                        var max = _RechtenLoopUp.GetType().GetProperties()
                            .Where(x => _userRoles.Contains(x.Name))
                            .OrderByDescending(a => a.GetValue(_RechtenLoopUp, null))
                            .FirstOrDefault().GetValue(_RechtenLoopUp, null);

                        // init rechten voor deze control
                        _ComboBox.InitEigenschappen(Convert.ToSByte(max));
                    }
                    return;
                }
                if (_Control is CalendarVeld)
                {
                    CalendarVeld _CalendarVeld = (CalendarVeld)_Control;

                    PropertyInfo prop = _Type.GetProperty(_CalendarVeld.DataBinding);
                    Attribute[] attributes = (Attribute[])prop.GetCustomAttributes(typeof(Attribute), true);

                    Attribute attribute = attributes.Where(x => x.GetType().Name.Equals("RECHTEN_LOOKUP")).FirstOrDefault();

                    if (attribute != null)
                    {
                        sbyte TableId = (sbyte)attribute.GetType().GetProperty("TableId").GetValue(attribute, null);
                        sbyte Value = (sbyte)attribute.GetType().GetProperty("Value").GetValue(attribute, null);

                        // rechten van betreffende control ophalen
                        RechtenLoopUp _RechtenLoopUp = _RechtenLoopUpList.Where(x => x.TableId == TableId && x.Value == Value).FirstOrDefault();

                        // grootste getal (recht) filtreren
                        var max = _RechtenLoopUp.GetType().GetProperties()
                            .Where(x => _userRoles.Contains(x.Name))
                            .OrderByDescending(a => a.GetValue(_RechtenLoopUp, null))
                            .FirstOrDefault().GetValue(_RechtenLoopUp, null);

                        // init rechten voor deze control
                        _CalendarVeld.InitEigenschappen(Convert.ToSByte(max));
                    }
                    return;
                }
            }
            catch (Exception err)
            {
                //
            }
        }

        public static void ControlBinder<T>(ControlCollection _col, T _obj)
        {
            foreach (Control _Control in _col)
            {
                InitThisControl(_Control, _obj);
            }
        }

        public static void ObjectBinder<T>(ControlCollection _col, T _obj)
        {
            foreach (Control _Control in _col)
            {
                InitThisObjectProperty(_Control, _obj);
            }
        }

        private static void InitThisObjectProperty<T>(Control _Control, T _obj)
        {
            Type _Type = typeof(T);

            if (_Control is LabelCheckBox)
            {
                LabelCheckBox _LabelCheckBox = (LabelCheckBox)_Control;
                if (_LabelCheckBox.DataBinding != null)
                {
                    var x_Query = typeof(T).GetProperties()
                        .Where(x => x.Name.ToLower().Equals(_LabelCheckBox.DataBinding.ToLower())).FirstOrDefault();

                    if (x_Query != null)
                    {
                        x_Query.SetValue(_obj, Convert.ChangeType(_LabelCheckBox.IsCheck, x_Query.PropertyType), null);
                    }
                }
            }

            if (_Control is LabelTekstVeld)
            {
                LabelTekstVeld _LabelTekstVeld = (LabelTekstVeld)_Control;
                if (_LabelTekstVeld.DataBinding != null)
                {
                    var x_Query = typeof(T).GetProperties()
                        .Where(x => x.Name.ToLower().Equals(_LabelTekstVeld.DataBinding.ToLower())).FirstOrDefault();

                    if (x_Query != null)
                    {
                        x_Query.SetValue(_obj, Convert.ChangeType(_LabelTekstVeld.Waarde, x_Query.PropertyType), null);
                    }
                }
            }

            if (_Control is ImageUploadVeld)
            {
                //ImageUploadVeld _ImageUploadVeld = (ImageUploadVeld)_Control;
                //var x_Query = typeof(T).GetProperties()
                //    .Where(x => x.Name.ToLower().Equals(_ImageUploadVeld.DataBinding.ToLower())).FirstOrDefault();

                //if (x_Query != null)
                //{
                //    x_Query.SetValue(_obj, Convert.ChangeType(_LabelTekstVeld.Waarde, x_Query.PropertyType), null);
                //}
            }

            if (_Control is CalendarVeld)
            {
                CalendarVeld _CalendarVeld = (CalendarVeld)_Control;
                if (_CalendarVeld.DataBinding != null)
                {
                    var x_Query = typeof(T).GetProperties()
                        .Where(x => x.Name.ToLower().Equals(_CalendarVeld.DataBinding.ToLower())).FirstOrDefault();

                    if (x_Query != null)
                    {
                        string date_Value = (_CalendarVeld.Waarde.Trim() == "") ? "1-1-0001" : _CalendarVeld.Waarde.Trim();
                        x_Query.SetValue(_obj, Convert.ChangeType(date_Value, x_Query.PropertyType), null);
                    }
                }
            }

            if (_Control is ComboBox)
            {
                ComboBox _ComboBox = (ComboBox)_Control;
                if (_ComboBox.DataBinding != null)
                {
                    var x_Query = typeof(T).GetProperties()
                        .Where(x => x.Name.ToLower().Equals(_ComboBox.DataBinding.ToLower())).FirstOrDefault();

                    if (x_Query != null)
                    {
                        x_Query.SetValue(_obj, Convert.ChangeType(_ComboBox.GekozenWaarde, x_Query.PropertyType), null);
                    }
                }
            }
        }

        private static void InitThisControl<T>(Control _Control, T _obj)
        {
            Type _Type = typeof(T);

            try
            {
                if (_Control is LabelTekstVeld)
                {
                    LabelTekstVeld _LabelTekstVeld = (LabelTekstVeld)_Control;
                    _LabelTekstVeld.Waarde = _Type.GetProperty(_LabelTekstVeld.DataBinding).GetValue(_obj, null).ToString();
                    return;
                }

                if (_Control is ImageUploadVeld)
                {
                    ImageUploadVeld _ImageUploadVeld = (ImageUploadVeld)_Control;
                    var value = _Type.GetProperty(_ImageUploadVeld.DataBinding).GetValue(_obj, null);
                    
                    bool i = value != null;
                    _ImageUploadVeld.FileUploadVisible = (!i);
                    _ImageUploadVeld.ButtonVisible = (i);
                    _ImageUploadVeld.ImageVisible = (i);

                    if (value != null)
                    {   
                        _ImageUploadVeld.ImageUrl = _ImageUploadVeld.ImageUrl.Replace("0", _Type.GetProperty(_ImageUploadVeld.ImageId).GetValue(_obj, null).ToString()) + "&t=" + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    }                    
                    return;
                }

                if (_Control is LabelCheckBox)
                {
                    LabelCheckBox _LabelCheckBox = (LabelCheckBox)_Control;
                    _LabelCheckBox.IsCheck = Convert.ToBoolean(_Type.GetProperty(_LabelCheckBox.DataBinding).GetValue(_obj, null));
                    return;
                }

                if (_Control is CalendarVeld)
                {
                    CalendarVeld _CalendarVeld = (CalendarVeld)_Control;

                    string Date_Value = Convert.ToDateTime(_Type.GetProperty(_CalendarVeld.DataBinding).GetValue(_obj, null).ToString()).ToShortDateString();
                    _CalendarVeld.Waarde = (Date_Value == "1-1-0001") ? "" : Date_Value;
                    return;
                }

                if (_Control is ComboBox)
                {
                    ComboBox _ComboBox = (ComboBox)_Control;
                    string DefaultValue = (string.IsNullOrEmpty(_ComboBox.DefaultWaarde)) ? string.Empty : _ComboBox.DefaultWaarde;

                    if (_ComboBox.ItemsField != null)
                    {
                        NameValueList<NameValue> x = (NameValueList<NameValue>)_Type.GetProperty(_ComboBox.ItemsField).GetValue(_obj, null);
                        
                        _ComboBox.ClearAllItems();

                        foreach (NameValue item in x)
                        {
                            _ComboBox.AddItem(new ListItem(item.Name, item.Value.ToString()));
                        }

                        _ComboBox.DataBind();
                        
                        // Hier wordt opgeslagen waarde uit database gehaald.
                        if (_Type.GetProperty(_ComboBox.DataBinding).GetValue(_obj, null) != null)
                            DefaultValue = _Type.GetProperty(_ComboBox.DataBinding).GetValue(_obj, null).ToString();

                        _ComboBox.GekozenWaarde = DefaultValue;
                    }
                    return;
                }
            }
            catch (Exception err)
            {
                //
            }
            
        }
    }
}
