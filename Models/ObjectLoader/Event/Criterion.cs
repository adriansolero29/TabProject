using Base;
using Base.PropertyBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectLoader.Event
{
    public class Criterion : ModelBase
    {
        public override string SqL => @"
SELECT JSONB_AGG(res)
FROM
(
    SELECT
    JSONB_BUILD_OBJECTS()
)
SELECT 
    ctn.""Id"", ctn.""Version"", ctn.""Id"", ctn.""Name"", ctn.""Sequence"", ctn.""Percentage"", ctn.""CreatedOn"", ctn.""ModifiedOn"",
    ct.""Id"", ct.""Version"", ct.""CriteriaName"", ct.""Sequence"", ct.""Percentage"", ct.""CreatedOn"", ct.""ModifiedOn"",
    cont.""Id"", cont.""Version"", cont.""Name"", cont.""CreatedOn"", cont.""ModifiedOn"", cont.""DateFrom"", cont.""DateTo"", cont.""Place""
FROM ""Event"".""Criterions"" ctn
LEFT OUTER JOIN ""Event"".""Criterias"" ct ON ct.""Id"" = ctn.""CriteriaId""
LEFT OUTER JOIN ""Event"".""Contest"" cont ON cont.""Id"" = ct.""ContestId""
WHERE ctn.""IsDeleted"" = FALSE
";
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

        private Criteria? _criteria;
        public Criteria? Criteria
        {
            get
            {
                return _criteria ?? new Criteria();
            }
            set
            {
                _criteria = value;
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
