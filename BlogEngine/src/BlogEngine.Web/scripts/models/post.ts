    export interface Post
    {
        id: number;
        title: string;
        content: string;
        postedOn: Date;
        isPublished: boolean;
        authorId: number;
        slug: string;
    }