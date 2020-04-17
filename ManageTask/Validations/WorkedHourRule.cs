using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace ManageTask.Validations
{
    public class WorkedHourRule : ValidationRule
    {
        private int min;
        private int max;

        public int Min {
            get => min;
            set => min = value;
        }

        public int Max {
            get => max;
            set => max = value;
        }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double TotalWorkedHour = 0;
            if (value.ToString() == "0-10" || value.ToString() == "") {
                return new ValidationResult(false,"Please Enter Worked Hours");
            }
            if (!double.TryParse(value.ToString(), out TotalWorkedHour))
            {
                return new ValidationResult(false, "Please Enter Valid Worked Hours!");
            }

            if (TotalWorkedHour < Min || TotalWorkedHour > Max)
            {
                return new ValidationResult(false, "Worked Hours Must Be Between 0 And 10.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
