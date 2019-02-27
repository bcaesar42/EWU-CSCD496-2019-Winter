<template>
    <div>
        <div class="title">Posts</div>
        <span style="float:right"><button class='button' @click="createPost()">Create Post</button></span>
        <table class='table'>
            <thead>
                <th>Title</th>
                <th>Is Published</th>
                <th>Posted On</th>
                <th></th>
            </thead>
            <tbody>
                <tr v-for="post in posts" :key="post.id">
                    <td>{{post.title}}</td>
                    <td><input type='checkbox' v-model='post.isPublished' readonly /></td>
                    <td>{{post.postedOn}}</td>
                    <td><button class='button' @click='setSelected(post)'>Edit</button></td>
                </tr>
            </tbody>
        </table>
        <div>
            <post-detail-component v-if="selectedPost != null" :post="selectedPost" @post-saved='refreshPosts()' />
        </div>
    </div>
</template>

<script lang="ts">
import {Vue, Component, Prop} from 'vue-property-decorator';
import { Post } from '../../models';
import PostDetailComponent from './post.vue';
import axios from 'axios';
@Component({
    components: {
        PostDetailComponent
    }
})
export default class Posts extends Vue {
    posts: Post[] = null;
    selectedPost: Post = null;

    setSelected(post: Post) {
        this.selectedPost = post;
    }

    constructor() {
        super();

        // this.posts = [
        //     {id: 1, title: 'Hello World', content: 'Here is some content that is really cool', isPublished: false, postedOn: new Date()},
        //     {id: 2, title: 'Another Post', content: 'Here is some more content that I will be using for demo', isPublished: true, postedOn: new Date()}
        // ];
    }

    createPost() {
        this.selectedPost = {} as any;
    }

    mounted() {
        // need to get data
        axios.get('https://localhost:44383/api/posts').then(response => this.posts = response.data);
        // alert('fetching data');
    }

    refreshPosts() {
        debugger;
        axios.get('https://localhost:44383/api/posts').then(response => this.posts = response.data);
        this.selectedPost = null;
    }
}
</script>