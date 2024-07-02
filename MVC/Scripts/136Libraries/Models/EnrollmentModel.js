function EnrollmentModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.Create = function (enrollment, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Enrollment/InsertEnrollment",
            data: enrollment,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding enrollment.  Is your service layer running?');
            }
        });
    };

    this.Get = function (scheduleid, callback) {
        var url = "http://localhost:9393/api/Enrollment/GetEnrollmentInfo?scheduleId=" + scheduleid;
        $.ajax({
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while getting enrollment.  Is your service layer running?');
            }
        });

    };

    this.Delete = function (studentid, scheduleid, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Enrollment/DeleteEnrollment?studentId=" + studentid
                +"&scheduleId="+scheduleid,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing enrollment.  Is your service layer running?');
            }
        });
    };
}
