using System;
using static System.Console;

namespace COR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var atm = new ATM();
            WriteLine(atm.CashOut(2033, CurrencyType.Dollar)); // True 20x100 + 1x20 + 1x10 + 1x2 + 1x1 
            WriteLine(atm.CashOut(2055, CurrencyType.Ruble)); // False 
        }
    }

    public enum CurrencyType
    {
        //Euro,
        Dollar,
        Ruble
    }

    public interface IBankNote
    {
        CurrencyType Currency { get; }
        string Value { get; }
    }

    public class ATM
    {
        private readonly BanknoteHandler _handler;

        public ATM()
        {
            _handler = new TenRubleHandler(_handler);
            _handler = new FiftyRubleHandler(_handler);
            _handler = new HundredRubleHandler(_handler);
            _handler = new TwoHundredRubleHandler(_handler);
            _handler = new FiveHundredRubleHandler(_handler);
            _handler = new ThousandRubleHandler(_handler);
            _handler = new TwoThousandRubleHandler(_handler);
            _handler = new OneDollarHandler(_handler);
            _handler = new TwoDollarHandler(_handler);
            _handler = new FiveThousandRubleHandler(_handler);
            _handler = new TenDollarHandler(_handler);
            _handler = new TwentyDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
        }
        public bool Validate(string banknote)
        {
            return _handler.Validate(banknote);
        }
        public bool CashOut(int amount, CurrencyType currency)
        {
            return _handler.CashOut(amount, currency);
        }
    }


    public abstract class BanknoteHandler
    {
        private readonly BanknoteHandler _nextHandler;

        protected BanknoteHandler(BanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public virtual bool Validate(string banknote)
        {
            return _nextHandler != null && _nextHandler.Validate(banknote);
        }

        public virtual bool CashOut(int amount, CurrencyType currency)
        {
            return _nextHandler != null && _nextHandler.CashOut(amount, currency);
        }
    }

    public abstract class RubleHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.Equals($"{Value} Рублей"))
            {
                return true;
            }
            return base.Validate(banknote);
        }

        protected abstract int Value { get; }

        protected RubleHandlerBase(BanknoteHandler nextHandler) : base(nextHandler) { }

        public override bool CashOut(int amount, CurrencyType currency)
        {
            if ((currency == CurrencyType.Ruble && amount == Value) || amount == 0)
                return true;
            return base.CashOut(amount - ((int)Math.Floor((double)amount / Value) * Value), currency);
        }

    }


    public abstract class DollarHandlerBase : BanknoteHandler
    {
        public override bool Validate(string banknote)
        {
            if (banknote.Equals($"{Value}$"))
            {
                return true;
            }
            return base.Validate(banknote);
        }

        protected abstract int Value { get; }

        protected DollarHandlerBase(BanknoteHandler nextHandler) : base(nextHandler)
        {
        }

        public override bool CashOut(int amount, CurrencyType currency)
        {
            if ((currency == CurrencyType.Dollar && amount == Value) || amount == 0)
                return true;
            return base.CashOut(amount - ((int)Math.Floor((double)amount / Value) * Value), currency);
        }
    }

    public class HundredDollarHandler : DollarHandlerBase
    {
        protected override int Value => 100;

        public HundredDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }

    }

    public class OneDollarHandler : DollarHandlerBase
    {
        protected override int Value => 1;

        public OneDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }

    }

    public class TwoDollarHandler : DollarHandlerBase
    {
        protected override int Value => 2;

        public TwoDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }

    }


    public class FiveDollarHandler : DollarHandlerBase
    {
        protected override int Value => 5;

        public FiveDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }

    }

    public class TwentyDollarHandler : DollarHandlerBase
    {
        protected override int Value => 20;

        public TwentyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }

    }

    public class FiftyDollarHandler : DollarHandlerBase
    {
        protected override int Value => 50;

        public FiftyDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class TenDollarHandler : DollarHandlerBase
    {
        protected override int Value => 10;

        public TenDollarHandler(BanknoteHandler nextHandler) : base(nextHandler)
        { }
    }

    public class TenRubleHandler : RubleHandlerBase
    {
        protected override int Value => 10;
        public TenRubleHandler(BanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class FiftyRubleHandler : RubleHandlerBase
    {
        protected override int Value => 50;
        public FiftyRubleHandler(BanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class HundredRubleHandler : RubleHandlerBase
    {
        protected override int Value => 100;
        public HundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class TwoHundredRubleHandler : RubleHandlerBase
    {
        protected override int Value => 200;
        public TwoHundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class FiveHundredRubleHandler : RubleHandlerBase
    {
        protected override int Value => 500;
        public FiveHundredRubleHandler(BanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class ThousandRubleHandler : RubleHandlerBase
    {
        protected override int Value => 1000;
        public ThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class TwoThousandRubleHandler : RubleHandlerBase
    {
        protected override int Value => 2000;
        public TwoThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler) { }
    }

    public class FiveThousandRubleHandler : RubleHandlerBase
    {
        protected override int Value => 5000;
        public FiveThousandRubleHandler(BanknoteHandler nextHandler) : base(nextHandler) { }
    }
}