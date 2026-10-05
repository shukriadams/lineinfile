namespace LineInFile
{
    public class LineIndexResponse : Response
    {
        public int Index {get; set;}
        
        public override string ToString()
        {
            return $"{base.ToString()}\nIndex:{this.Index}";
        }
    }    
}