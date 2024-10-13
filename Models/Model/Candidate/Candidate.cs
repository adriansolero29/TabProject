using Base.PropertyBase;
using Model.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Candidate
{
    public class Candidate : ModelBase
    {
        public override string SqL => "";
        public override string SqlCount => "";
        public override string SqlInsert => "";
        public override string SqlUpdate => "";
        public override string SqlHardDelete => "";

        private Guid? _id;
        public Guid? Id
        {
            get { return _id; }
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

        private Person? _person;
        public Person? Person
        {
            get
            {
                return _person ?? new Person();
            }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        private CandidateTypeSection? _candidateTypeSection;
        public CandidateTypeSection? CandidateTypeSection
        {
            get
            {
                return _candidateTypeSection ?? new CandidateTypeSection();
            }
            set
            {
                _candidateTypeSection = value;
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

        private string? _candidateNo;
        public string? CandidateNo
        {
            get
            {
                return _candidateNo;
            }
            set
            {
                _candidateNo = value;
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
