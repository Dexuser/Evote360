using Evote360.Core.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EVote360.Helpers
{
    public class MessageHandler 
    {
        public static void Handle(Result result, Controller controller)
        {
            switch (result.MessageType)
            {
                case MessageType.Field:
                    controller.ModelState.AddModelError(result.FieldName, result.Error!);
                    break;

                case MessageType.Summary:
                    controller.ModelState.AddModelError("", result.Error!);
                    break;

                case MessageType.Alert:
                    controller.ViewBag.Message = result.Error!;
                    break;
            }
        }
    }
}
