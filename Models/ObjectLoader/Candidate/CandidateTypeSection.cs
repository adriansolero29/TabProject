using Base.PropertyBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectLoader.Candidate
{
    public class CandidateTypeSection : ModelBase
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

        private CandidateType? _candidateType;
        public CandidateType? CandidateType
        {
            get
            {
                return _candidateType ?? new CandidateType();
            }
            set
            {
                _candidateType = value;
                OnPropertyChanged();
            }
        }

        private string? _definition1;
        public string? Definition1
        {
            get
            {
                return _definition1;
            }
            set
            {
                _definition1 = value;
                OnPropertyChanged();
            }
        }

        private string? _definition2;
        public string? Definition2
        {
            get
            {
                return _definition2;
            }
            set
            {
                _definition2 = value;
                OnPropertyChanged();
            }
        }

        private string? _definition3;
        public string? Definition3
        {
            get
            {
                return _definition3;
            }
            set
            {
                _definition3 = value;
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
