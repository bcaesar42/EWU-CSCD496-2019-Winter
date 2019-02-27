import '../styles/site.scss';
import Vue from 'vue';
import PostsComponent from './components/posts/posts.vue';

let v = new Vue({
    el: '#posts',
    template: `
        <div>
            <posts-component />
        </div>`,
    components: {
        PostsComponent
    }
});