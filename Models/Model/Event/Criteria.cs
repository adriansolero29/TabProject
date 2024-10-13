using Base;
using Base.PropertyBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Event
{
    public class Criteria : ModelBase
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

        private Contest? _contest;
        public Contest? Contest
        {
            get
            {
                return _contest ?? new Contest();
            }
            set
            {
                _contest = value;
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
