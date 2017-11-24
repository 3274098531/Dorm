namespace MyFramework.Domain
{
    public class Version : Entity
    {
        public string SystemName { get; set; }
        public double Num { get; set; }

        public void AddNum(double? num)
        {
            Num += num??0.01;
        }

        public void InitVersion()
        {
            Num = 1.0;
        }
    }
}