// brand.js
var app = getApp()

Page({
  data: {
    productList: [],
    loading: false,
    pageFinished: false,
    currentPage: 1,
    totalPage: 0,
    pageSize: 20,
    sdkVersion: app.getSDKVersion()
  },

  getProduct: function (cb) {
    var that = this
    if (that.data.loading == false) {
      that.setData({
        loading: true
      })
      wx.request({
        url: app.globalData.apiUrl + '/api/getlist',
        data: {
          types: "0",
          stype: 2,
          size: that.data.pageSize,
          page: that.data.currentPage,
          name: ''
        },
        method: "post",
        header: {
          'content-type': 'application/json'
        },
        success: function (res) {
          if (res.data.code == 0) {
            if (res.data.data.length > 0) {
              if (res.data.total == 1 || res.data.total==that.data.currentPage) {
                that.setData({
                  pageFinished: true
                });
              }
              that.setData({
                productList: that.data.productList.concat(res.data.data),
                totalPage: res.data.total,
                currentPage: that.data.currentPage + 1
              });
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

  onReachBottom: function () {
    var that = this
    if (that.data.currentPage > that.data.totalPage) {
      that.setData({
        pageFinished: true
      });
      return;
    }
    that.getProduct()
  },

  onLoad: function () {
    var that = this
    wx.setNavigationBarTitle({
      title: '优购8-实惠'
    })
    that.getProduct()
  },

  onPullDownRefresh: function () {
    this.setData({
      currentPage: 1,
      productList: [],
      pageFinished: false
    })
    this.getProduct(function () { wx.stopPullDownRefresh() })
  }

})