namespace ApiCliente.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T data, List<string> erros)
        {
            Data = data;
            Erros = erros;
        }

        public ResultViewModel(T data)
        {
            Data = data;
        }

        public ResultViewModel(string error)
        {
            Erros.Add(error);
        }

        public ResultViewModel(List<string> erros)
        {
            Erros = erros;
        }

        public T Data { get; private set; }
        public List<string> Erros { get; private set; } = new();
    }
}
