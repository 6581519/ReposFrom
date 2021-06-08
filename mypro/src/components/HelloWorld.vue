<template>
  <div class="hello">
    <el-row>
      <el-col :span="8"
        ><div class="grid-content bg-purple">
          All Repos from
          <el-link type="info">https://github.com/idcf-boat-house</el-link>
        </div></el-col
      >
      <el-col :span="8"
        ><div class="grid-content bg-purple-light"></div
      ></el-col>
      <el-col :span="8"
        ><div class="grid-content bg-purple">My Faor Repos</div></el-col
      >
    </el-row>
    <el-row>
      <el-col :span="8">
        <div class="grid-content bg-purple">
          <el-table
            ref="multipleTable"
            :data="tableDataleft"
            tooltip-effect="dark"
            style="width: 100%"
            @selection-change="handleSelectionChangeLeft"
          >
            <el-table-column type="selection" width="55"> </el-table-column>
            <el-table-column prop="Name" label="名称" width="120">
            </el-table-column>
          </el-table>
        </div>
      </el-col>
      <el-col :span="8"
        ><div class="grid-content bg-purple">
          <el-button type="primary" v-on:click="GetLeft">初始化数据</el-button>
          <el-button type="primary" v-on:click="AddToFavorites"
            >添加收藏</el-button
          >
          <el-button type="primary" v-on:click="DeleteToFavorites"
            >删除收藏</el-button
          >
        </div></el-col
      >
      <el-col :span="8"
        ><div class="grid-content bg-purple">
          <el-table
            ref="multipleTable"
            :data="tableDataright"
            tooltip-effect="dark"
            style="width: 100%"
            @selection-change="handleSelectionChangeRight"
          >
            <el-table-column type="selection" width="55"> </el-table-column>
            <el-table-column prop="Name" label="名称" width="120">
            </el-table-column>
          </el-table></div
      ></el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <div class="grid-content bg-purple-dark">
          <el-button type="primary" v-on:click="GetRight"
            >Generate Email</el-button
          >
        </div>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="24">
        <div class="grid-content bg-purple-dark">
          <el-input
            type="textarea"
            :rows="2"
            placeholder="Hello Alan
            boat-house
            IDCF boat-house 社区共创文档库, 官网： https://boat-house.cn/ 文档库：https://idcf.org.cn/boat-house/#/
            boat-house-devopsbox
            单机版DevOps工具链环境，适合于开发者进行DevOps实践学习，开发，测试和实验。"
            
            v-model="textarea"
          >
          </el-input>
        </div>
      </el-col>
    </el-row>
  </div>
</template>
<script src="https://unpkg.com/axios/dist/axios.min.js"></script>

<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      textarea: "",
      tableDataleft: [
        {
          Name: "boat-house",
        },
        {
          Name: "boat-house-devopsbox",
        },
      ],
      tableDataright: [
        {
          Name: "boat-house-frontend",
        },
        {
          Name: "boat-house-backend",
        },
      ],
      multipleSelectionLeft: [],
      multipleSelectionRight: [],
    };
  },
  methods: {
    AddToFavorites: function () {
      console.log(this.multipleSelectionLeft);
    },
    DeleteToFavorites: function () {
      console.log(this.multipleSelectionRight);
    },
    Getinitialization: function () {
      this.$http.get("http://localhost:50740/api/v1/Warehouse/StockItem").then(
        function (res) {
          console.log(res);
        },
        function () {
          console.log("请求失败处理");
        }
      );
    },
    GetLeft: function () {
      const _this = this;
      this.$axios
        .get("http://localhost:50740/api/v1/Warehouse/GetLeft/")
        .then(function (resp) {
          console.log(resp.data);
        });
    },
    AddRight: function (Name) {
      this.$http
        .get("http://localhost:50740/api/v1/Warehouse/AddRight/" + Name)
        .then(
          function (res) {
            console.log(res.body);
          },
          function () {
            console.log("请求失败处理");
          }
        );
    },
    DeleteRight: function (Name) {
      this.$http
        .get("http://localhost:50740/api/v1/Warehouse/DeleteRight/" + Name)
        .then(
          function (res) {
            console.log(res.body);
          },
          function () {
            console.log("请求失败处理");
          }
        );
    },
    GetRight: function () {
      this.$http.get("http://localhost:50740/api/v1/Warehouse/GetRight/").then(
        function (res) {
          console.log(res.body);
        },
        function () {
          console.log("请求失败处理");
        }
      );
    },
    handleSelectionChangeLeft(val) {
      this.multipleSelectionLeft = val;
    },
    handleSelectionChangeRight(val) {
      this.multipleSelectionRight = val;
    },
  },
  mounted: function () {
    //this.Getinitialization();
    this.GetLeft();
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.el-row {
  margin-bottom: 20px;
}
.el-col {
  border-radius: 4px;
}

.grid-content {
  border-radius: 4px;
  min-height: 36px;
}
.row-bg {
  padding: 10px 0;
  background-color: #f9fafc;
}
</style>
