namespace Lince.Exceptions
{
    public class ConflitException : Exception
    {
        public ConflitException(String mensagem) 
            : base(mensagem) 
        { }
    }
    
    public class NotFoundException : Exception
    {
        public NotFoundException(String mensagem) 
            : base(mensagem) 
        { }
    }
}