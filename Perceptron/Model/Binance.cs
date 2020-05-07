namespace Perceptron.Model
{
    public class Binance
    {
        //create table binance
        //( id serial primary key, param decimal, weight decimal, datetime timestamp, bidask boolean, value decimal);

        private int _id = 0;
        public int id
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        private decimal _param = 0;
        public decimal param
        {
            get { return _param; }
            set
            {
                _param = value;
            }
        }

        private decimal _weight = 0;
        public decimal weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
            }
        }

        private decimal _value = 0;
        public decimal value
        {
            get { return _value; }
            set
            {
                value = _value;
            }
        }

        private string _datetime = "";
        public string datetime
        {
            get { return _datetime; }
            set
            {
                _datetime = value;
            }
        }

        private bool _bidask = true;
        public bool bidask
        {
            get { return _bidask; }
            set
            {
                _bidask = value;
            }
        }

    }
}
