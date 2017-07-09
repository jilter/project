// include.js
var app = getApp()

Page({
  data: {
    freeList: [],
    loading: false,
    pageFinished: false,
    currentPage: 1,
    totalPage: 0,
    pageSize: 20,
    sdkVersion: app.getSDKVersion()
  },

  getFreeList: function (cb) {
    var that = this
    if (that.data.loading == false) {
      that.setData({
        loading: true
      })
      wx.request({
        url: app.globalData.apiUrl+'/api/getfreelist',
        data: {
          size: that.data.pageSize,
          page: that.data.currentPage
        },
        method: "post",
        header: {
          'content-type': 'application/json'
        },
        success: function (res) {
          console.log(res.data.data)
          if (res.data.code == 0) {
            if (res.data.data.length > 0) {
              if (res.data.total == 1 || res.data.total == that.data.currentPage) {
                that.setData({
                  pageFinished: true
                });
              }
              that.setData({
                freeList: that.data.freeList.concat(res.data.data),
                totalPage: res.data.total,
                currentPage: that.data.currentPage + 1
              })
            } else {
              that.setData({
                pageFinished: true
              });
            }
          }
        },
        fail: function (res) {
          console.log(res);
        },
        complete: function (res) {
          that.setData({
            loading: false
          })
          typeof cb == "function" && cb()
          console.log(res);
        }
      })
    }
  },

  onLoad: function () {
    var that = this
    wx.setNavigationBarTitle({
      title: '优购8-赠品'
    })
    that.getFreeList();
  },

  onReachBottom: function () {
    if (this.data.currentPage > this.data.totalPage) {
      this.setData({
        pageFinished: true
      });
      return;
    }
    this.getFreeList()
  },

  onPullDownRefresh: function () {
    this.setData({
      currentPage: 1,
      freeList: [],
      pageFinished: false
    })
    this.getFreeList(function () { wx.stopPullDownRefresh() })
  }
})