//// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
//// Due to async nature of ajax, the Jasmine's compare function would throw an error during
//// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
//// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
//// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
//// be async when called by viewModel.
function ForeignStudentModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.Create = function (foreignStudent, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/ForeignStudent/InsertForeignStudent",
            data: foreignStudent,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding foreign student.  Is your service layer running?');
            }
        });
    };

    this.Update = function (foreignStudent, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/ForeignStudent/UpdateForeignStudent",
            data: foreignStudent,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while updating a foreign student.  Is your service layer running?');
            }
        });
    };

    this.Delete = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/ForeignStudent/DeleteForeignStudent?id=" + id,
            data: '',
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing foreign student.  Is your service layer running?');
            }
        });
    };

    this.GetAll = function (callback) {
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: "http://localhost:9393/api/ForeignStudent/GetForeignStudentList",
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading foreign student list.  Is your service layer running?');
            }
        });
    };

    this.GetDetail = function (id, callback) {
        var url = "http://localhost:9393/Api/ForeignStudent/GetForeignStudent?id=" + id + "&bust=" + new Date();

        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading foreign student detail.  Is your service layer running?');
            }
        });
    };
}

