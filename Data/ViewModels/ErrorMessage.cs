namespace AngularCore.Data.ViewModels
{
    public class ErrorMessage
    {
        public string Message { get; set; }

        public ErrorMessage( string message ) {
            this.Message = message;
        }
    }
}