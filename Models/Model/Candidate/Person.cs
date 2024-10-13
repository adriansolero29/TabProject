using Base.PropertyBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Candidate
{
    public class Person : ModelBase
    {
        public override string SqL => "";
        public override string SqlCount => "";
        public override string SqlInsert => "";
        public override string SqlUpdate => "";
        public override string SqlHardDelete => "";

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

        private string? _first;
        public string? First
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
                OnPropertyChanged();
            }
        }

        private string? _middle;
        public string? Middle
        {
            get
            {
                return _middle;
            }
            set
            {
                _middle = value;
                OnPropertyChanged();
            }
        }

        private string? _last;
        public string? Last
        {
            get
            {
                return _last;
            }
            set
            {
                _last = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _birthdate;
        public DateTime? Birthdate
        {
            get
            {
                return _birthdate;
            }
            set
            {
                _birthdate = value;
                OnPropertyChanged();
            }
        }

        private string? _address;
        public string? Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
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
