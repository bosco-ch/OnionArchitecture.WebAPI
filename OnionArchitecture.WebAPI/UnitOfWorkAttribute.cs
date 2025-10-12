namespace OnionArchitecture.WebAPI
{
    [AttributeUsage(AttributeTargets.Method)]//表示 只能应用于方法
    public class UnitOfWorkAttribute : Attribute
    {
        public Type[] DBContextTypes { get; init; }
        public UnitOfWorkAttribute(params Type[] dbContextTypes)//params 允许你在调用方法时传入任意数量的指定类型的参数（包括零个），这些参数会被编译器自动封装成一个数组。
        {
            this.DBContextTypes = dbContextTypes;
        }
    }
}
