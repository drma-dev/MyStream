using System.Collections.Generic;

namespace MyStream.Modal
{
    public class ProviderBase
    {
        public int display_priority { get; set; }
        public string logo_path { get; set; }
        public int provider_id { get; set; }
        public string provider_name { get; set; }
        public string provider_desciption { get; set; }
        public string provider_link { get; set; }
    }

    public class Flatrate : ProviderBase { }

    public class FlatrateAndBuy : ProviderBase { }

    public class Rent : ProviderBase { }

    public class Buy : ProviderBase { }

    public class CountryBase
    {
        public string link { get; set; }
        public List<Flatrate> flatrate { get; set; }
        public List<FlatrateAndBuy> flatrate_and_buy { get; set; }
        public List<Rent> rent { get; set; }
        public List<Buy> buy { get; set; }
    }

    public class AR : CountryBase { }

    public class AT : CountryBase { }

    public class AU : CountryBase { }

    public class BE : CountryBase { }

    public class BG : CountryBase { }

    public class BR : CountryBase { }

    public class CA : CountryBase { }

    public class CH : CountryBase { }

    public class CL : CountryBase { }

    public class CO : CountryBase { }

    public class CZ : CountryBase { }

    public class DE : CountryBase { }

    public class DK : CountryBase { }

    public class EC : CountryBase { }

    public class EE : CountryBase { }

    public class ES : CountryBase { }

    public class FI : CountryBase { }

    public class FR : CountryBase { }

    public class GB : CountryBase { }

    public class GR : CountryBase { }

    public class HU : CountryBase { }

    public class ID : CountryBase { }

    public class IE : CountryBase { }

    public class IN : CountryBase { }

    public class IT : CountryBase { }

    public class JP : CountryBase { }

    public class KR : CountryBase { }

    public class LT : CountryBase { }

    public class LV : CountryBase { }

    public class MX : CountryBase { }

    public class MY : CountryBase { }

    public class NL : CountryBase { }

    public class NO : CountryBase { }

    public class NZ : CountryBase { }

    public class PE : CountryBase { }

    public class PH : CountryBase { }

    public class PL : CountryBase { }

    public class PT : CountryBase { }

    public class RO : CountryBase { }

    public class RU : CountryBase { }

    public class SE : CountryBase { }

    public class SG : CountryBase { }

    public class TH : CountryBase { }

    public class TR : CountryBase { }

    public class US : CountryBase { }

    public class VE : CountryBase { }

    public class ZA : CountryBase { }

    public class Results
    {
        public AR AR { get; set; }
        public AT AT { get; set; }
        public AU AU { get; set; }
        public BE BE { get; set; }
        public BG BG { get; set; }
        public BR BR { get; set; }
        public CA CA { get; set; }
        public CH CH { get; set; }
        public CL CL { get; set; }
        public CO CO { get; set; }
        public CZ CZ { get; set; }
        public DE DE { get; set; }
        public DK DK { get; set; }
        public EC EC { get; set; }
        public EE EE { get; set; }
        public ES ES { get; set; }
        public FI FI { get; set; }
        public FR FR { get; set; }
        public GB GB { get; set; }
        public GR GR { get; set; }
        public HU HU { get; set; }
        public ID ID { get; set; }
        public IE IE { get; set; }
        public IN IN { get; set; }
        public IT IT { get; set; }
        public JP JP { get; set; }
        public KR KR { get; set; }
        public LT LT { get; set; }
        public LV LV { get; set; }
        public MX MX { get; set; }
        public MY MY { get; set; }
        public NL NL { get; set; }
        public NO NO { get; set; }
        public NZ NZ { get; set; }
        public PE PE { get; set; }
        public PH PH { get; set; }
        public PL PL { get; set; }
        public PT PT { get; set; }
        public RO RO { get; set; }
        public RU RU { get; set; }
        public SE SE { get; set; }
        public SG SG { get; set; }
        public TH TH { get; set; }
        public TR TR { get; set; }
        public US US { get; set; }
        public VE VE { get; set; }
        public ZA ZA { get; set; }
    }

    public class TMDB_Providers
    {
        public int id { get; set; }
        public Results results { get; set; }
    }

    public class TMDB_AllProviders
    {
        public List<ProviderBase> results { get; set; }
    }
}