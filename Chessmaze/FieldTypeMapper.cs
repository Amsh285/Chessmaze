using System;

namespace Chessmaze
{
    public static class FieldTypeMapper
    {
        //public static char MapType(FieldType value)
        //{
        //    switch (value)
        //    {
        //        case FieldType.Empty:
        //            return '.';
        //        case FieldType.Cluster:
        //            return 'C';
        //        case FieldType.Obstacle:
        //            return 'X';
        //        case FieldType.Road:
        //            return 'R';
        //        case FieldType.Start:
        //            return 'S';
        //        case FieldType.End:
        //            return 'E';
        //        case FieldType.Node:
        //            return 'N';
        //        default:
        //            throw new NotSupportedException($"FieldType: {value} is not supported.");
        //    }
        //}

        public static char MapType(FieldType value)
        {
            switch (value)
            {
                case FieldType.Empty:
                    return ' ';
                case FieldType.Cluster:
                    return 'C';
                case FieldType.Obstacle:
                    return 'X';
                case FieldType.Road:
                    return '.';
                case FieldType.Start:
                    return 'S';
                case FieldType.End:
                    return 'E';
                case FieldType.Node:
                    return 'O';
                default:
                    throw new NotSupportedException($"FieldType: {value} is not supported.");
            }
        }
    }
}
