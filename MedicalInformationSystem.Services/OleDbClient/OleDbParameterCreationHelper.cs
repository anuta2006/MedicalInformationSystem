using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Services.OleDbClient
{
    public static class OleDbParameterCreationHelper
    {
        private const int StringParameterSizeInBytes = 50;

        public static OleDbParameter AsOleDbInputParameter(this string value, string parameterName)
            => CreateInputParameter(parameterName, OleDbType.VarChar, value);

        public static OleDbParameter AsOleDbInputParameter(this int value, string parameterName)
            => CreateInputParameter(parameterName, OleDbType.Integer, value);

        public static OleDbParameter AsOleDbInputParameter(this DateTime value, string parameterName)
            => CreateInputParameter(parameterName, OleDbType.Date, value.Date);


        private static OleDbParameter CreateInputParameter(string parameterName, OleDbType oleDbType, object value)
            => new OleDbParameter(parameterName, oleDbType) { Direction = ParameterDirection.Input, Value = value };

        private static OleDbParameter CreateOutputParameter(string parameterName, OleDbType oleDbType)
           => new OleDbParameter(parameterName, oleDbType) { Direction = ParameterDirection.Output };

    }
}
