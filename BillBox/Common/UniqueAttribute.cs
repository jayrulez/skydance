using BillBox.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Common
{
    public class UniqueAttribute : ValidationAttribute
    {
        private string _entityName;
        private string _compareFiled;
        private Entities _dbContext;

        public UniqueAttribute(string EntityName, string CompareField)
        {
            _dbContext    = new Entities();
            _entityName   = EntityName;
            _compareFiled = CompareField;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                if (true)
                {
                    /*
                     var agent = _dbContext.Agents.Any(a => a.Name = value);
                     if(agent != null) {
                        return new ValidationResult("Name is already used");
                     }
                     */
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return base.IsValid(value, validationContext);
        }
    }
}