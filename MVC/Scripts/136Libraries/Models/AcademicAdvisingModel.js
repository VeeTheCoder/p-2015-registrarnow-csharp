//// THe reason for asyncIndicator is to make sure Jasmine test cases can run without error
//// Due to async nature of ajax, the Jasmine's compare function would throw an error during
//// a callback. By allowing this optional paramter for StudentModel function, it forces the ajax
//// call to be synchronous when running the Jasmine tests.  However, the viewModel will not pass
//// this parameter so the asynncIndicator would be undefined which is set to "true". Ajax would
//// be async when called by viewModel.
function AcademicAdvisingModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }
    
    this.Create = function (academicAdvising, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/AcademicAdvising/InsertAdvisingMessage",
            data: academicAdvising,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding academic advising message.  Is your service layer running?');
            }
        });
    };

    this.GetMessage = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: "http://localhost:9393/Api/AcademicAdvising/GetAdvisingMessageDetail?id=" + id,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading academicAdvising message get detail.  Is your service layer running?');
            }
        });
    };

    this.Load = function (id, callback) {
        $.ajax({
            method: 'GET',
            url: "http://localhost:9393/Api/AcademicAdvising/GetAdvisingMessageDetail?id=" + id,
            data: "",
            dataType: "json",
            success: function (result) {
             
                callback(result); 
            },
            error: function () {
                alert('Error while loading advising info.');
                callback("Error while loading advising info");
            }
        });
    };

    this.Update = function (academicAdvisingData, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/Api/AcademicAdvising/UpdateAdvisingMessage",
            data: academicAdvisingData, // note, adminData must be the same as PLAdmin for this to work correctly
            success: function (message) {
                callback(message);
                alert('Update successful!');
            },
            error: function () {
                callback('Error while updating admin info');
            }
        });

    };

    this.Delete = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/AcademicAdvising/DeleteAdvisingMessage?id=" + id,
            data: '',
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing student.  Is your service layer running?');
            }
        });
    };
    
    this.GetAll = function (studentId, isInstructor, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: "http://localhost:9393/Api/AcademicAdvising/GetAdvisingMessages?studentId=" + studentId + "&isInstructor=" + isInstructor,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading academicAdvising list.  Is your service layer running?');
            }
        });
    };
    
  
}
