using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace OnionArchitecture.WebAPI
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var result = await next();//执行action方法
            //需要加判断
            if (result.Exception != null)//如果action有异常，
            {
                return;
            }
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;//进行显示转换    
            if (actionDesc == null)
            {
                return;
            }
            var uowAttr = actionDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();//看看这个方法上面有没有标注 UnitOfWorkAttribute 
            if (uowAttr == null)//没有标注的话，就不处理
            {
                return;
            }
            foreach (var type in uowAttr.DBContextTypes)
            {
                var dbctx = context.HttpContext.RequestServices.GetService(type) as DbContext;//通过DI 拿到DBContext实例
                if (dbctx != null)//拿到实例
                {
                    await dbctx.SaveChangesAsync();
                }

            }
        }
    }
}
