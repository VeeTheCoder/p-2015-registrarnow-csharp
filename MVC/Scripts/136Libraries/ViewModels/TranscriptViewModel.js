function TranscriptViewModel() {

    function processViewObj(arr) {
        if (arr.length > 0) {
            var currentYear = arr[0].Year;
            var retArray = ko.observableArray();
            retArray.push({ value: currentYear, isBold: 'bold', wip: ''});
           
            for (var i = 0; i < arr.length; i++) {
                if (currentYear == arr[i].Year) {
                    if (arr[i].Grade) {
                        retArray.push({ value: arr[i].CourseTitle + ' - ' + arr[i].Grade, isBold: '', wip: '' });
                    } else {
                        retArray.push({ value: arr[i].CourseTitle + ' (Work in Progress)', isBold: '', wip: 'red' });
                    }
                } else {
                    currentYear = arr[i].Year;
                    retArray.push({ value: currentYear, isBold: 'bold', wip: '' });
                    if (arr[i].Grade) {
                        retArray.push({ value: arr[i].CourseTitle + ' - ' + arr[i].Grade, isBold: '', wip: '' });
                    } else {
                        retArray.push({ value: arr[i].CourseTitle + ' (Work in Progress)', isBold: '', wip: 'red' });
                    }
                }
            }
            return retArray;
        }
        return ko.observableArray();
    }

    this.Load = function (id) {
        var studentModelObj = new StudentModel();

        studentModelObj.GetEnrollments(id,function (enrollmentListData) {

            var enrollmentList = ko.observableArray(enrollmentListData);

            enrollmentList.sort(function (left, right) {
                return left.Year == right.Year ? 0 : (left.Year < right.Year ? -1 : 1)
            });

            var transcriptViewModel = processViewObj(enrollmentList());

            // this is using knockoutjs to bind the viewModel and the view (Home/Index.cshtml)
            ko.applyBindings({ viewModel: transcriptViewModel }, document.getElementById("divTranscriptContent"));
        });
    };
}