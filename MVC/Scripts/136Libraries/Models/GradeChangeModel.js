function GradeChangeModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }
    
    this.Create = function (gradeChange, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/GradeChange/InsertGradeChangeMessage",
            data: gradeChange,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while inserting grade change reuqest. Is your service layer running?');
            }
        });
    };

    this.Update = function (gradeChangeData, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/Api/GradeChange/UpdateGradeChangeMessage",
            data: gradeChangeData, 
            success: function (message) {
                callback(message);
                alert('Update successful!');
            },
            error: function () {
                callback('Error while updating grade change status');
            }
        });

    };

    this.GetAll = function (studentId, isInstructor, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: "http://localhost:9393/Api/GradeChange/GetGradeChangeMessages?studentId=" + studentId + "&isInstructor=" +isInstructor,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while retrieving grade change message list.  Is your service layer running?');
            }
        });
    };
    
    this.GetDetail = function (id, callback) {
        var url = "http://localhost:9393/Api/GradeChange/GetGradeChangeMessageDetail?id=" + id;

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
                alert('Error while loading grade change message.  Is your service layer running?');
            }
        });
    };

    this.Delete = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/GradeChange/DeleteGradeChangeMessage?id=" + id,
            data: '',
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing grade change request.  Is your service layer running?');
            }
        });
    };
}
