using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GeoLib.Contracts;
using GeoLib.Data;

namespace GeoLib.Services
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class GeoManager : IGeoService
    {
        private readonly IZipCodeRepository _zipCodeRepository;
        private readonly IStateRepository _stateRepository;

        public GeoManager()
            : this(new StateRepository(), new ZipCodeRepository())
        {
        }

        public GeoManager(IStateRepository stateRepository = null, IZipCodeRepository zipCodeRepository = null)
        {
            _zipCodeRepository = zipCodeRepository;
            _stateRepository = stateRepository;
        }

        public ZipCodeData GetZipInfo(string zip)
        {
            // Use for timeout
            //Thread.Sleep(5000);
            throw new Exception("You can't touch this!");

            ZipCodeData zipCodeData = null;

            ZipCode zipCodeEntity = _zipCodeRepository.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }

            return zipCodeData;
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            var stateData = new List<string>();

            var states = _stateRepository.Get(primaryOnly);
            if (states != null)
            {
                stateData.AddRange(states.Select(x => x.Abbreviation));
            }

            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            var zipCodeData = new List<ZipCodeData>();

            var zipCodeEntities = _zipCodeRepository.GetByState(state);
            if (zipCodeEntities != null)
            {
                foreach (var zipCodeEntity in zipCodeEntities)
                {
                    zipCodeData.Add(new ZipCodeData()
                    {
                        City = zipCodeEntity.City,
                        State = zipCodeEntity.State.Abbreviation,
                        ZipCode = zipCodeEntity.Zip
                    });
                }
            }

            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            var zipCodeData = new List<ZipCodeData>();

            var zipCode = _zipCodeRepository.GetByZip(zip);
            var zipCodeEntities = _zipCodeRepository.GetZipsForRange(zipCode, range);
            if (zipCodeEntities != null)
            {
                foreach (var zipCodeEntity in zipCodeEntities)
                {
                    zipCodeData.Add(new ZipCodeData()
                    {
                        City = zipCodeEntity.City,
                        State = zipCodeEntity.State.Abbreviation,
                        ZipCode = zipCodeEntity.Zip
                    });
                }
            }

            return zipCodeData;
        }
    }
}
