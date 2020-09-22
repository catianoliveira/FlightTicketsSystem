using Flights.Web.Helpers;
using Flights.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FlightTicketsSystem.Web.Data.Entities;

namespace Flights.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;


        //TODO aauthorize
        //User manager faz a gestão dos users
        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        //TODO ver user manager

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("SuperAdmin");
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Employee");
            await _userHelper.CheckRoleAsync("Client");


            if (!_context.Countries.Any())
            {

                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                List<RegionInfo> countriesList = new List<RegionInfo>();
                var countries = new List<Country>();
                foreach (CultureInfo ci in cultures)
                {
                    RegionInfo regionInfo = new RegionInfo(ci.Name);
                    if (countriesList.Count(x => x.EnglishName == regionInfo.EnglishName) <= 0)
                    {
                        countriesList.Add(regionInfo);
                    }
                }

                foreach (RegionInfo regionInfo in countriesList.OrderBy(x => x.EnglishName))
                {
                    var country = regionInfo.EnglishName;
                    AddCountries(country);

                    await _context.SaveChangesAsync();
                }

                AddCountries("null");
            }

            if (!_context.Airplanes.Any())
            {
                this.AddAirplanes("Airbus 123", 2, 15, 5);
                this.AddAirplanes("Boeing 787", 3, 30, 15);
                this.AddAirplanes("Boeing 781", 1, 40, 18);

                await _context.SaveChangesAsync();
            }



            if (!_context.Airports.Any())
            {
                this.AddAirports("Lisbon", "Portugal", "LIS");
                this.AddAirports("Oporto", "Portugal", "OPO");
                this.AddAirports("Berlin", "Germany", "BER");
                this.AddAirports("Madrid", "Spain", "MAD");
                this.AddAirports("Dublin", "Ireland", "DUB");
            }




            if (!_context.Indicatives.Any())
            {
                this.AddIndicatives("Afghanistan", "+93");
                this.AddIndicatives("Albania", "+355");
                this.AddIndicatives("Algeria", "+213");
                this.AddIndicatives("American Samoa", "+1684");
                this.AddIndicatives("Andorra", "+376");
                this.AddIndicatives("Angola", "+244");
                this.AddIndicatives("Anguilla  ", "+1264");
                this.AddIndicatives("Antarctica(Australian bases) ", "+6721");
                this.AddIndicatives("Antigua and Barbuda", "+1268");
                this.AddIndicatives("Argentina", "+54");
                this.AddIndicatives("Armenia", "+374");
                this.AddIndicatives("Aruba", "+297");
                this.AddIndicatives("Ascension", "+247");
                this.AddIndicatives("Australia", "+61");
                this.AddIndicatives("Austria", "+43");
                this.AddIndicatives("Azerbaijan", "+994");
                this.AddIndicatives("Bahamas", "+1242");
                this.AddIndicatives("Bahrain", "+973");
                this.AddIndicatives("Bangladesh", "+880");
                this.AddIndicatives("Barbados", "+1246");
                this.AddIndicatives("Belarus", "+375");
                this.AddIndicatives("Belgium", "+32");
                this.AddIndicatives("Belize", "+501");
                this.AddIndicatives("Benin", "+229");
                this.AddIndicatives("Bermuda", "+1441");
                this.AddIndicatives("Bhutan", "+975");
                this.AddIndicatives("Bolivia", "+591");
                this.AddIndicatives("Bosnia and Herzegovina", "+387");
                this.AddIndicatives("Botswana", "+267");
                this.AddIndicatives("Brazil", "+55");
                this.AddIndicatives("British Indian Ocean Territory", "+246");
                this.AddIndicatives("British Virgin Islands", "+1284");
                this.AddIndicatives("Brunei", "+673");
                this.AddIndicatives("Bulgaria", "+359");
                this.AddIndicatives("Burkina Faso", "+226");
                this.AddIndicatives("Burundi", "+257");
                this.AddIndicatives("Cambodia", "+855");
                this.AddIndicatives("Cameroon", "+237");
                this.AddIndicatives("Canada", "+1");
                this.AddIndicatives("Cape Verde", "+238");
                this.AddIndicatives("Cayman Islands", "+1345");
                this.AddIndicatives("Central African Republic", "+236");
                this.AddIndicatives("Chad", "+235");
                this.AddIndicatives("Chile", "+56");
                this.AddIndicatives("China", "+86");
                this.AddIndicatives("Colombia", "+57");
                this.AddIndicatives("Comoros", "+269");
                this.AddIndicatives("Congo, Democratic Republic of the", "+243");
                this.AddIndicatives("Congo, Republic of the", "+242");
                this.AddIndicatives("Cook Islands", "+682");
                this.AddIndicatives("Costa Rica", "+506");
                this.AddIndicatives("Cote dIvoire", "+225");
                this.AddIndicatives("Croatia", "+385");
                this.AddIndicatives("Cuba", "+53");
                this.AddIndicatives("Curaao", "+599");
                this.AddIndicatives("Cyprus", "+357");
                this.AddIndicatives("Czech Republic", "+420");
                this.AddIndicatives("Denmark", "+45");
                this.AddIndicatives("Djibouti", "+253");
                this.AddIndicatives("Dominica", "+1767");
                this.AddIndicatives("Dominican Republic", "+1809");
                this.AddIndicatives("Ecuador", "+593");
                this.AddIndicatives("Egypt", "+20");
                this.AddIndicatives("El Salvador", "+503");
                this.AddIndicatives("Equatorial Guinea", "+240");
                this.AddIndicatives("Eritrea", "+291");
                this.AddIndicatives("Estonia", "+372");
                this.AddIndicatives("Eswatini", "+268");
                this.AddIndicatives("Ethiopia", "+251");
                this.AddIndicatives("Falkland Islands", "+500");
                this.AddIndicatives("Faroe Islands", "+298");
                this.AddIndicatives("Fiji", "+679");
                this.AddIndicatives("Finland", "+358");
                this.AddIndicatives("France", "+33");
                this.AddIndicatives("French Guiana", "+594");
                this.AddIndicatives("French Polynesia", "+689");
                this.AddIndicatives("Gabon", "+241");
                this.AddIndicatives("Gambia", "+220");
                this.AddIndicatives("Gaza Strip", "+970");
                this.AddIndicatives("Georgia", "+995");
                this.AddIndicatives("Germany", "+49");
                this.AddIndicatives("Ghana", "+233");
                this.AddIndicatives("Gibraltar", "+350");
                this.AddIndicatives("Greece", "+30");
                this.AddIndicatives("Greenland", "+299");
                this.AddIndicatives("Grenada", "+1473");
                this.AddIndicatives("Guadeloupe", "+590");
                this.AddIndicatives("Guam", "+1671");
                this.AddIndicatives("Guatemala", "+502");
                this.AddIndicatives("Guinea", "+224");
                this.AddIndicatives("GuineaBissau", "+245");
                this.AddIndicatives("Guyana", "+592");
                this.AddIndicatives("Haiti", "+509");
                this.AddIndicatives("Honduras", "+504");
                this.AddIndicatives("Hong Kong", "+852");
                this.AddIndicatives("Hungary", "+36");
                this.AddIndicatives("Iceland", "+354");
                this.AddIndicatives("India", "+91");
                this.AddIndicatives("Indonesia", "+62");
                this.AddIndicatives("Iraq", "+964");
                this.AddIndicatives("Iran", "+98");
                this.AddIndicatives("Ireland ", "+353");
                this.AddIndicatives("Israel", "+972");
                this.AddIndicatives("Italy", "+39");
                this.AddIndicatives("Jamaica", "+1876");
                this.AddIndicatives("Japan", "+81");
                this.AddIndicatives("Jordan", "+962");
                this.AddIndicatives("Kazakhstan", "+7");
                this.AddIndicatives("Kenya", "+254");
                this.AddIndicatives("Kiribati", "+686");
                this.AddIndicatives("Kosovo", "+383");
                this.AddIndicatives("Kuwait", "+965");
                this.AddIndicatives("Kyrgyzstan", "+996");
                this.AddIndicatives("Laos", "+856");
                this.AddIndicatives("Latvia", "+371");
                this.AddIndicatives("Lebanon", "+961");
                this.AddIndicatives("Lesotho", "+266");
                this.AddIndicatives("Liberia", "+231");
                this.AddIndicatives("Libya", "+218");
                this.AddIndicatives("Liechtenstein", "+423");
                this.AddIndicatives("Lithuania", "+370");
                this.AddIndicatives("Luxembourg", "+352");
                this.AddIndicatives("Macau", "+853");
                this.AddIndicatives("Madagascar", "+261");
                this.AddIndicatives("Malawi", "+265");
                this.AddIndicatives("Malaysia", "+60");
                this.AddIndicatives("Maldives", "+960");
                this.AddIndicatives("Mali", "+223");
                this.AddIndicatives("Malta", "+356");
                this.AddIndicatives("Marshall Islands", "+692");
                this.AddIndicatives("Martinique", "+596");
                this.AddIndicatives("Mauritania", "+222");
                this.AddIndicatives("Mauritius", "+230");
                this.AddIndicatives("Mayotte", "+262");
                this.AddIndicatives("Mexico", "+52");
                this.AddIndicatives("Micronesia, Federated States of", "+691");
                this.AddIndicatives("Moldova ", "+373");
                this.AddIndicatives("Monaco", "+377");
                this.AddIndicatives("Mongolia", "+976");
                this.AddIndicatives("Montenegro", "+382");
                this.AddIndicatives("Montserrat ", "+1664");
                this.AddIndicatives("Morocco", "+212");
                this.AddIndicatives("Mozambique", "+258");
                this.AddIndicatives("Myanmar", "+95");
                this.AddIndicatives("Namibia", "+264");
                this.AddIndicatives("Nauru", "+674");
                this.AddIndicatives("Netherlands", "+31");
                this.AddIndicatives("Netherlands Antilles", "+599");
                this.AddIndicatives("Nepal", "+977");
                this.AddIndicatives("New Caledonia", "+687");
                this.AddIndicatives("New Zealand", "+64");
                this.AddIndicatives("Nicaragua", "+505");
                this.AddIndicatives("Niger", "+227");
                this.AddIndicatives("Nigeria", "+234");
                this.AddIndicatives("Niue", "+683");
                this.AddIndicatives("Norfolk Island", "+6723");
                this.AddIndicatives("North Korea", "+850");
                this.AddIndicatives("North Macedonia", "+389");
                this.AddIndicatives("Northern Ireland", "+4428");
                this.AddIndicatives("Northern Mariana Islands", "+1670");
                this.AddIndicatives("Norway", "+47");
                this.AddIndicatives("Oman", "+968");
                this.AddIndicatives("Pakistan", "+92");
                this.AddIndicatives("Palau", "+680");
                this.AddIndicatives("Panama", "+507");
                this.AddIndicatives("Papua New Guinea", "+675");
                this.AddIndicatives("Paraguay", "+595");
                this.AddIndicatives("Peru", "+51");
                this.AddIndicatives("Philippines", "+63");
                this.AddIndicatives("Poland", "+48");
                this.AddIndicatives("Portugal", "+351");
                this.AddIndicatives("Puerto Rico", "+1787");
                this.AddIndicatives("Qatar", "+974");
                this.AddIndicatives("Reunion", "+262");
                this.AddIndicatives("Romania", "+40");
                this.AddIndicatives("Russia", "+7");
                this.AddIndicatives("Rwanda", "+250");
                this.AddIndicatives("SaintBarthlemy", "+590");
                this.AddIndicatives("Saint HelenaandTristan da Cunha", "+290");
                this.AddIndicatives("Saint Kitts and Nevis", "+1869");
                this.AddIndicatives("Saint Lucia", "+1758");
                this.AddIndicatives("Saint Martin(French side) ", "+590");
                this.AddIndicatives("Saint Pierre and Miquelon", "+508");
                this.AddIndicatives("Saint Vincent and the Grenadines", "+1784");
                this.AddIndicatives("Samoa", "+685");
                this.AddIndicatives("Sao Tome and Principe", "+239");
                this.AddIndicatives("Saudi Arabia", "+966");
                this.AddIndicatives("Senegal", "+221");
                this.AddIndicatives("Serbia", "+381");
                this.AddIndicatives("Seychelles", "+248");
                this.AddIndicatives("Sierra Leone", "+232");
                this.AddIndicatives("Sint Maarten(Dutch side) ", "+1721");
                this.AddIndicatives("Singapore", "+65");
                this.AddIndicatives("Slovakia", "+421");
                this.AddIndicatives("Slovenia", "+386");
                this.AddIndicatives("Solomon Islands", "+677");
                this.AddIndicatives("Somalia", "+252");
                this.AddIndicatives("South Africa", "+27");
                this.AddIndicatives("South Korea", "+82");
                this.AddIndicatives("South Sudan", "+211");
                this.AddIndicatives("Spain", "+34");
                this.AddIndicatives("Sri Lanka", "+94");
                this.AddIndicatives("Sudan", "+249");
                this.AddIndicatives("Suriname", "+597");
                this.AddIndicatives("Sweden", "+46");
                this.AddIndicatives("Switzerland", "+41");
                this.AddIndicatives("Syria", "+963");
                this.AddIndicatives("Taiwan", "+886");
                this.AddIndicatives("Tajikistan", "+992");
                this.AddIndicatives("Tanzania", "+255");
                this.AddIndicatives("Thailand", "+66");
                this.AddIndicatives("TimorLeste", "+670");
                this.AddIndicatives("Togo", "+228");
                this.AddIndicatives("Tokelau", "+690");
                this.AddIndicatives("Tonga", "+676");
                this.AddIndicatives("Trinidad and Tobago", "+1868");
                this.AddIndicatives("Tunisia", "+216");
                this.AddIndicatives("Turkey", "+90");
                this.AddIndicatives("Turkmenistan", "+993");
                this.AddIndicatives("Turks and Caicos Islands", "+1649");
                this.AddIndicatives("Tuvalu", "+688");
                this.AddIndicatives("Uganda", "+256");
                this.AddIndicatives("Ukraine", "+380");
                this.AddIndicatives("United Arab Emirates", "+971");
                this.AddIndicatives("United Kingdom", "+44");
                this.AddIndicatives("United States of America", "+1");
                this.AddIndicatives("Uruguay", "+598");
                this.AddIndicatives("Uzbekistan", "+998");
                this.AddIndicatives("Vanuatu", "+678");
                this.AddIndicatives("Venezuela", "+58");
                this.AddIndicatives("Vietnam", "+84");
                this.AddIndicatives("U.S. Virgin Islands", "+1340");
                this.AddIndicatives("Wallis and Futuna", "+681");
                this.AddIndicatives("West Bank", "+970");
                this.AddIndicatives("Yemen", "+967");
                this.AddIndicatives("Zambia", "+260");
                this.AddIndicatives("Zimbabwe", "+263");

                await _context.SaveChangesAsync();
            }



            var user = await _userHelper.GetUserByEmailAsync("ctoliveira44@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Cátia",
                    LastName = "Oliveira",
                    Email = "ctoliveira44@gmail.com",
                    UserName = "ctoliveira44@gmail.com",
                    IndicativeId = 171,
                    PhoneNumber = "912345678",
                    Address = "Rua da Luz 1 2ºEsq 1200-110 Lisboa",
                    City = "Lisboa",
                    CountryId = 179,
                    EmailConfirmed = true,
                    //IsActive = true
                };

                var result = await _userHelper.AddUserAsync(user, "123456");



                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }


                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);


                var isInRole = await _userHelper.IsUserInRoleAsync(user, "SuperAdmin");
                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "SuperAdmin");
                }

                await _context.SaveChangesAsync();
            }
        }


        private void AddIndicatives(
        string country, string code)
        {

            _context.Indicatives.Add(new Indicative
            {
                Country = country,
                Code = code
            });
        }


        private void AddAirports(
        string city, string country,
        string iata)
        {

            _context.Airports.Add(new Airport
            {
                City = city,
                Country = country,
                IATA = iata
            });
        }

        private void AddAirplanes(
            string model, int quantity,
            int economy, int business)
        {
            _context.Airplanes.Add(new Airplane
            {
                Model = model,
                EconomySeats = economy,
                BusinessSeats = business,
            });
        }



        private void AddCountries(string name)
        {
            _context.Countries.Add(new Country
            {
                Name = name
            });
        }
    }
}
