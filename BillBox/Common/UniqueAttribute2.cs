using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


public class UniqueAttribute2 : ValidationAttribute
{
    public UniqueAttribute2(Type dataContextType, Type entityType, string propertyName)
    {
        DataContextType = dataContextType;
        EntityType = entityType;
        PropertyName = propertyName;
    }


    public Type DataContextType { get; private set; }


    public Type EntityType { get; private set; }


    public string PropertyName { get; private set; }


    public override bool IsValid(object value)
    {
        // Construct the data context
        ConstructorInfo constructor = DataContextType.GetConstructor(new Type[0]);
        DataContext dataContext = (DataContext)constructor.Invoke(new object[0]);


        // Get the table
        ITable table = dataContext.GetTable(EntityType);


        // Get the property
        PropertyInfo propertyInfo = EntityType.GetProperty(PropertyName);


        // Our ultimate goal is an expression of:
        //   "entity => entity.PropertyName == value"


        // Expression: "value"
        object convertedValue = Convert.ChangeType(value, propertyInfo.PropertyType);
        ConstantExpression rhs = Expression.Constant(convertedValue);


        // Expression: "entity"
        ParameterExpression parameter = Expression.Parameter(EntityType, "entity");


        // Expression: "entity.PropertyName"
        MemberExpression property = Expression.MakeMemberAccess(parameter, propertyInfo);


        // Expression: "entity.PropertyName == value"
        BinaryExpression equal = Expression.Equal(property, rhs);


        // Expression: "entity => entity.PropertyName == value"
        LambdaExpression lambda = Expression.Lambda(equal, parameter);


        // Instantiate the count method with the right TSource (our entity type)
        MethodInfo countMethod = QueryableCountMethod.MakeGenericMethod(EntityType);


        // Execute Count() and say "you're valid if you have none matching"
        int count = (int)countMethod.Invoke(null, new object[] { table, lambda });
        return count == 0;
    }


    // Gets Queryable.Count<TSource>(IQueryable<TSource>, Expression<Func<TSource, bool>>)
    private static MethodInfo QueryableCountMethod = typeof(Queryable).GetMethods().First(m => m.Name == "Count" && m.GetParameters().Length == 2);
}