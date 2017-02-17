
Namespace Common

    ''' <summary>
    ''' Transaction State Enumura
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TransState
        Add = 1 'data add
        Update = 2 'data update
        Delete = 3 'data delete
        UpdateNotPossible = 4 'data update no possible due any business rule
        Exist = 5 'data already exist
        NoRecord = 6 'no record found
        DBError = 7 'database error 
        UnspecifiedError = 8 'unspecificied error
        MakerCheckerSame = 9
        AlreadyAuthorized = 10
        ModifiedOutside = 11 ' modified after the information is loaded
        AlreadyDeleted = 12
        AlreadyPaid = 13
        ChildExist = 14
        ParentNotExist = 15

    End Enum

    Public Enum FormTransMode
        Add = 1 'data Add Mode
        Update = 2 'data update Mode
        Search = 3 'data search mode 
        AddMas = 4 'data add Mode for master table

    End Enum

End Namespace