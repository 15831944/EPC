using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocSystem.Logic
{
    public class ControlGenrator
    {
        public static IControl GenrateMiniControl(string ctType)
        {
            IControl control;

            if (ctType == ControlType.TextBox.ToString() || ctType == ControlType.TextBoxFullRow.ToString())
            {
                control = new MiniTextBox();
                if (ctType == ControlType.TextBoxFullRow.ToString())
                    control.Style = " width:100% ";
            }
            else if (ctType == ControlType.TextArea.ToString() || ctType == ControlType.TextAreaFullRow.ToString())
                control = new MiniTextArea();
            else if (ctType == ControlType.DatePicker.ToString())
                control = new MiniDatePicker();
            else if (ctType == ControlType.Combobox.ToString() || ctType == ControlType.ComboboxFullRow.ToString())
            {
                control = new MiniComboBox();
                if (ctType == ControlType.ComboboxFullRow.ToString())
                    control.Style = " width:100% ";
            }
            else if (ctType == ControlType.ButtonEdit.ToString() || ctType == ControlType.ButtonEditFullRow.ToString())
            {
                control = new MiniButtonEdit();
                if (ctType == ControlType.ButtonEditFullRow.ToString())
                    control.Style = " width:100% ";
            }
            else if (ctType == ControlType.SingleFile.ToString() || ctType == ControlType.SingleFileFullRow.ToString())
            {
                control = new MiniFileSingle();
                if (ctType == ControlType.SingleFileFullRow.ToString())
                    control.Style = " width:100% ";
            }
            else if (ctType == ControlType.MultiFile.ToString() || ctType == ControlType.MultiFileFullRow.ToString())
            {
                control = new MiniFileMulti();
                if (ctType == ControlType.MultiFileFullRow.ToString())
                    control.Style = " width:100% ";
            }
            else
                control = new MiniTextBox();            
            return control;
        }

        public static Table CreateDefaultQueryFormTable()
        {
            Table table = new Table();
            TableRow row = new TableRow();
            for (int k = 0; k < 6; k++)
            {
                TableCell cell = new TableCell();
                if (k % 2 == 0)
                    cell.Width = 10;
                else
                    cell.Width = 23;
                row.AddControl(cell);
            }
            table.AddControl(row);
            return table;
 
        }

        public static Table CreateDefaultFormTable()
        {
            Table table = new Table();
            TableRow row = new TableRow();
            for (int k = 0; k < 4; k++)
            {
                TableCell cell = new TableCell();
                if (k % 2 == 0)
                    cell.Width = 15;
                else
                    cell.Width = 35;
                row.AddControl(cell);
            }
            table.AddControl(row);
            return table;
        }
    }
}
