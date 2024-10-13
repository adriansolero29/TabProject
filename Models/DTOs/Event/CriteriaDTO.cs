using Base;
using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Event
{
    public class CriteriaDTO : DTOModel
    {
        public override object? ToDto(object? objectLoader)
        {
            var objL = (Criteria?)objectLoader;
            return new CriteriaDTO
            {
                //Id = objL?.Id.ToString(),
                ModifiedByUserId  = objL?.ModifiedByUserId,
                Version = objL?.Version,
                ContestId = objL?.Contest?.Id,
                CriteriaName = objL?.CriteriaName,
                Sequence = objL?.Sequence,
                Percentage = objL?.Percentage,
                CreatedOn = objL?.CreatedOn,
                ModifiedOn = objL?.ModifiedOn,
            };
        }

        private Guid? _id;
        public Guid? Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private Guid? _modifiedByUserId;
        public Guid? ModifiedByUserId
        {
            get
            {
                return _modifiedByUserId;
            }
            set
            {
                _modifiedByUserId = value;
                OnPropertyChanged();
            }
        }

        private int? _version;
        public int? Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
                OnPropertyChanged();
            }
        }

        private Guid? _contestId;
        public Guid? ContestId
        {
            get
            {
                return _contestId;
            }
            set
            {
                _contestId = value;
                OnPropertyChanged();
            }
        }

        private string? _criteriaName;
        public string? CriteriaName
        {
            get
            {
                return _criteriaName;
            }
            set
            {
                _criteriaName = value;
                OnPropertyChanged();
            }
        }

        private int? _sequence;
        public int? Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
                OnPropertyChanged();
            }
        }

        private double? _percentage;
        public double? Percentage
        {
            get
            {
                return _percentage;
            }
            set
            {
                _percentage = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _createdOn;
        public DateTime? CreatedOn
        {
            get
            {
                return _createdOn;
            }
            set
            {
                _createdOn = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _modifiedOn;
        public DateTime? ModifiedOn
        {
            get
            {
                return _modifiedOn;
            }
            set
            {
                _modifiedOn = value;
                OnPropertyChanged();
            }
        }
    }
}
