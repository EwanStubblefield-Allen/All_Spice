import { Profile } from "./Profile.js"

export class Recipe {
  constructor(data) {
    this.id = data.id
    this.favoriteId = data.favoriteId ? data.favoriteId : null
    this.title = data.title
    this.instructions = data.instructions
    this.img = data.img
    this.category = data.category
    this.createdAt = new Date(data.createdAt).toLocaleString()
    this.updatedAt = new Date(data.updatedAt).toLocaleString()
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
  }
}