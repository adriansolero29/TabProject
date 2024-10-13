using ObjectLoader.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs.Event
{
    public class ContestDTO :DTOModel
    {
        public override object? ToDto(object? objectLoader)
        {
            var objL = (Contest?)objectLoader;

            return new ContestDTO ()
            {
                Id = objL?.Id,
                Name = objL?.Name,
                ModifiedByUserId = objL?.ModifiedByUserId,
                Version = objL?.Version,
                CreatedOn = objL?.CreatedOn,
                ModifiedOn = objL?.ModifiedOn,
                DateFrom = objL?.DateFrom,
                DateTo = objL?.DateTo,
                Place = objL?.Place,
                IsCurrentContest = objL?.IsCorrentContest,
                IsDeleted = objL?.IsDeleted,
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

        private string? _name;
        public string? Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
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

        private DateTime? _dateFrom;
        public DateTime? DateFrom
        {
            get
            {
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dateTo;
        public DateTime? DateTo
        {
            get
            {
                return _dateTo;
            }
            set
            {
                _dateTo = value;
                OnPropertyChanged();
            }
        }

        private string? _place;
        public string? Place
        {
            get
            {
                return _place;
            }
            set
            {
                _place = value;
                OnPropertyChanged();
            }
        }

        private bool? _isCurrentContest;
        public bool? IsCurrentContest
        {
            get
            {
                return _isCurrentContest;
            }
            set
            {
                _isCurrentContest = value;
                OnPropertyChanged();
            }
        }

        private bool? _isDeleted;
        public bool? IsDeleted
        {
            get
            {
                return _isDeleted;
            }
            set
            {
                _isDeleted = value;
                OnPropertyChanged();
            }
        }

    }
}
