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
FROM (
    SELECT 
    JSONB_BUILD_OBJECT
    (
        'Id', crtrn.""Id"", 
        'ModifiedByUserId', crtrn.""ModifiedByUserId"", 
        'Version', crtrn.""Version"", 
        'Name', crtrn.""Name"", 
        'Sequence', crtrn.""Sequence"",
        'Percentage', crtrn.""Percentage"", 
        'CreatedOn', crtrn.""CreatedOn"", 
        'ModifiedOn', crtrn.""ModifiedOn"",
        'Criteria', JSON_BUILD_OBJECT(
            'Id', cri.""Id"", 
            'ModifiedByUserId', cri.""ModifiedByUserId"", 
            'CriteriaName', cri.""CriteriaName"", 
            'Sequence', cri.""Sequence"",
            'Percentage', cri.""Percentage"", 
            'CreatedOn', cri.""CreatedOn"", 
            'ModifiedOn', cri.""ModifiedOn"",
            'Contest', JSON_BUILD_OBJECT(
                'Id', conts.""Id"", 
                'ModifiedByUserId', conts.""ModifiedByUserId"",
                'Version', conts.""Version"", 
                'Name', conts.""Name"", 
                'CreatedOn', conts.""CreatedOn"", 
                'ModifiedOn', conts.""ModifiedOn"", 
                'DateFrom', conts.""DateFrom"", 
                'DateTo', conts.""DateTo"", 
                'Place', conts.""Place""
            )
        )

    ) ""MainObject""
    FROM ""Event"".""Criterions"" crtrn
    LEFT OUTER JOIN ""Event"".""Criterias"" cri ON cri.""Id"" = crtrn.""CriteriaId""
    LEFT OUTER JOIN ""Event"".""Contest"" conts ON conts.""Id"" = cri.""ContestId""
    WHERE crtrn.""IsDeleted"" = FALSE
) res
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
                if (_criteria == null)
                    _criteria = new Criteria();
                return _criteria;
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
