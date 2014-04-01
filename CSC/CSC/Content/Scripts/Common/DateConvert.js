function strToDate(str) {
	if (str== undefined)
		return;
    var strSplit = str.split("-");
    var DateValue = new Date(strSplit[2], strSplit[1] - 1, strSplit[0]);
    return DateValue;
}