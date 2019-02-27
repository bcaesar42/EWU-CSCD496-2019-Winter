<template>
    <div>
        <div class="field">
            <label>Title</label>
            <div class="control">
                <input class="input" type="text" v-model="post.title" />
            </div>
        </div>
        <div class="field">
            <label>Content</label>
            <div class="control">
                <input class="input" type="text" v-model="post.content" />
            </div>
        </div>
        <div class="field">
            <label>Posted On</label>
            <div class="control">
                <input class="input" type="text" v-model="post.postedOn" />
            </div>
        </div>
        <div class="field">
            <label>Is Published</label>
            <div class="control">
                <input class="checkbox" type="checkbox" v-model="post.isPublished" />
            </div>
        </div>
        <div class="field">
            <label>SLUG</label>
            <div class="control">
                <input class="input" type="text" v-model="post.slug" />
            </div>
        </div>
        <div class="field">
            <label>Author ID</label>
            <div class="control">
                <input class="input" type="text" v-model="post.authorId" />
            </div>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button class="button is-primary" @click="save()">Save</button>
            </div>
            <div class="control">
                <a class="button is-light" @click="cancel()">Cancel</a>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import {Vue, Component, Prop, Emit} from 'vue-property-decorator';
import {Post} from '../../models';
import axios from 'axios';
@Component
export default class PostDetail extends Vue {
    @Prop() post: Post;

    save() {
        if (this.post.id > 0) {
            axios.put(`https://localhost:44383/api/posts?id=${this.post.id}`, this.post).then(() => alert('Post updated'));
        }
        else {
            axios.post('https://localhost:44383/api/posts', this.post).then(() => {
                alert('Post created');
                this.postSaved();
            });
        }
    }

    @Emit('post-saved')
    postSaved() {}

    cancel() {
    }
}
</script>