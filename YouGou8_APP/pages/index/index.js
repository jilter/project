var app = getApp()

Page({
  data: {
    imgUrls: ['https://63971804.qcloud.la/images/ad1.png', 'https://63971804.qcloud.la/images/ad2.png'],
    productList: [],
    indicatorDots: true,
    autoplay: true,
    interval: 3000,
    duration: 1000,
    loading: false,
    pageFinished: false,
    pageSize: 20,
    currentPage: 1,
    totalPage: 0,
    searchName: '',
    searchClick: false,
    sdkVersion: app.getSDKVersion()
  },
  onLoad: function () {
    var that = this
    that.getProduct()
  },
  onPullDownRefresh: function () {
    this.setData({
      currentPage: 1,
      productList: [],
      pageFinished: false
    })
    this.getProduct(function () { wx.stopPullDownRefresh() })
  },
  onReachBottom: function () {
    console.log('onReachBottom')
    if (this.data.currentPage > this.data.totalPage) {
      this.setData({
        loading: false,
        pageFinished: true
      })
      return
    }
    this.getProduct()
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
          stype: 1,
          size: that.data.pageSize,
          page: that.data.currentPage,
          name: that.data.searchName
        },
        method:"post",
        header: {
          'content-type': 'application/json'
        },
        success: function (res) {
          if (res.data.code == 0) {
            if (res.data.data.length > 0) {
              if (res.data.total == 1 || res.data.total == that.data.currentPage) {
                that.setData({
                  pageFinished: true
                })
              }
              that.setData({
                productList: that.data.productList.concat(res.data.data),
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
  clearInput: function () {
    this.setData({
      searchName: ""
    })
    if (this.data.searchClick) {
      this.searchProduct()
      this.setData({
        searchClick: false
      })
    }
  },
  inputTyping: function (e) {
    this.setData({
      searchName: e.detail.value
    });
  },
  searchProduct: function () {
    this.setData({
      currentPage: 1,
      productList: [],
      pageFinished: false,
      searchClick: true
    })
    this.getProduct()
  }
});