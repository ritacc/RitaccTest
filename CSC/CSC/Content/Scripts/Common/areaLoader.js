$(function () {
    $(".areaContainer").LevelLoader({
        data: [{ url: '/Province/Index', postKey: 'Search.ProvCode', objKey: 'Code', postName: 'ProvCode', htmlObj: 'levelLoaderX' }, { url: '/City/Index', postKey: 'Search.CityCode', objKey: 'Code', postName: 'CityCode', htmlObj: 'levelLoaderY' }, { url: '/Area/Index', postKey: 'Search.AreaCode', objKey: 'Code', postName: 'AreaCode', htmlObj: 'levelLoaderZ'}],
        getList: function (data) {
            return data.Result.List;
        },
        getItem: function (item) {
            return '<option value="' + item.Code + '">' + item.FullName + '</option>';
        },
        autoLoad:true
    });
});