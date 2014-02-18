using BillBox.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BillBox.Common
{
    public class UniqueAttribute : ValidationAttribute
    {
        public Type DataContextType;
        public Type EntityType;
        public string PropertyName;

        // Gets Queryable.Count<TSource>(IQueryable<TSource>, Expression<Func<TSource, bool>>)
        private static MethodInfo QueryableCountMethod = typeof(Queryable).GetMethods().First(m => m.Name == "Count" && m.GetParameters().Length == 2);

        public UniqueAttribute(Type dataContextType, Type entityType, string propertyName)
        {
            DataContextType = dataContextType;
            EntityType = entityType;
            PropertyName = propertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }

        //public override bool IsValid(object value)
        //{
        //    // Construct the data context
        //    ConstructorInfo constructor = DataContextType.GetConstructor(new Type[0]);
        //    DataContext dataContext = (DataContext)constructor.Invoke(new object[0]);

        //    // Get the table
        //    ITable table = dataContext.GetTable(EntityType);

        //    // Get the property
        //    PropertyInfo propertyInfo = EntityType.GetProperty(PropertyName);

        //    // Our ultimate goal is an expression of:
        //    //   "entity => entity.PropertyName == value"

        //    // Expression: "value"
        //    object convertedValue = Convert.ChangeType(value, propertyInfo.PropertyType);
        //    ConstantExpression rhs = Expression.Constant(convertedValue);

        //    // Expression: "entity"
        //    ParameterExpression parameter = Expression.Parameter(EntityType, "entity");

        //    // Expression: "entity.PropertyName"
        //    MemberExpression property = Expression.MakeMemberAccess(parameter, propertyInfo);

        //    // Expression: "entity.PropertyName == value"
        //    BinaryExpression equal = Expression.Equal(property, rhs);

        //    // Expression: "entity => entity.PropertyName == value"
        //    LambdaExpression lambda = Expression.Lambda(equal, parameter);

        //    // Instantiate the count method with the right TSource (our entity type)
        //    MethodInfo countMethod = QueryableCountMethod.MakeGenericMethod(EntityType);

        //    // Execute Count() and say "you're valid if you have none matching"
        //    int count = (int)countMethod.Invoke(null, new object[] { table, lambda });
        //    return count == 0;
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                // Construct the data context
                //ConstructorInfo constructor = DataContextType.GetConstructor(new Type[0]);
                //DataContext dataContext = (DataContext)constructor.Invoke(new object[0]);
                var repository = DependencyResolver.Current.GetService(DataContextType);

                //var repository = DependencyResolver.Current.GetService(typeof(IRepository<>).MakeGenericType(validationContext.ObjectType));

                var data = repository.GetType().InvokeMember("GetAll", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null, repository, null);

                

                // Get the table
                //ITable table = dataContext.GetTable(EntityType);


                // Get the property
                PropertyInfo propertyInfo = EntityType.GetProperty(PropertyName);


                // Our ultimate goal is an expression of:
                //   "entity => entity.PropertyName == value"


                // Expression: "value"
                object convertedValue = Convert.ChangeType(value, propertyInfo.PropertyType);
                var rhs = Expression.Constant(convertedValue);


                // Expression: "entity"
                var parameter = Expression.Parameter(EntityType, "entity");


                // Expression: "entity.PropertyName"
                var property = Expression.MakeMemberAccess(parameter, propertyInfo);


                // Expression: "entity.PropertyName == value"
                var equal = Expression.Equal(property, rhs);


                // Expression: "entity => entity.PropertyName == value"
                var lambda = Expression.Lambda(equal, parameter).Compile();

                // Instantiate the count method with the right TSource (our entity type)
                MethodInfo countMethod = QueryableCountMethod.MakeGenericMethod(EntityType);

                // Execute Count() and say "you're valid if you have none matching"
                int count = (int)countMethod.Invoke(null, new object[] { data, lambda });
                //return count == 0;

                if (count != 0)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
                else
                {
                    return ValidationResult.Success;
                }

                /*
                if (true)
                {
                   
                     var agent = _dbContext.Agents.Any(a => a.Name = value);
                     if(agent != null) {
                        return new ValidationResult("Name is already used");
                     }
                     
                }
                else
                {
                    return ValidationResult.Success;
                }
            */
            }

            return base.IsValid(value, validationContext);
        }
    }
}