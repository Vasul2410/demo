namespace demo1.model
{
    public class JsonResponse
    {
        public bool Success { get;}

        public string Message { get;}

        public object Result { get;}

        public JsonResponse(int StatusCode, bool? success = null, string message = null, object result = null)
        {
            Success = success ?? IsSuccessStatus(StatusCode);
            Message = message ?? GetDefaultMessage(StatusCode);
            Result = result;
               
        }


        private static bool IsSuccessStatus(int statusCode)
        {
            if (statusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static string GetDefaultMessage(int statusCode)
        {
            switch (statusCode) 
            { 
            case 301:
                    return "Product Name Is Already Taken ";
            case 302:
                    return "Enter The Valid Values";
            }
            return "";
        }
    }
}
