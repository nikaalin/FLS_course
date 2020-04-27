package com.example.cipher2.controllers

import com.example.cipher2.models.TextModel
import com.github.kittinunf.fuel.Fuel
import com.github.kittinunf.fuel.gson.responseObject
import kotlinx.coroutines.Dispatchers.IO
import kotlinx.coroutines.withContext

class StringCipherRequest(
    private val key: String,
    private val doc: String?
) : ICipherRequest() {
    override var response: TextModel = TextModel("", "", "")
    override var status: Int = 777


    override suspend fun sendEncrypt() {
        post(encryptUrl(key, "string"))
    }

    override suspend fun sendDecrypt() {
        post(decryptUrl(key, "string"))
    }

    private suspend fun post(url: String) {
            val (_, response, result) = withContext(IO) {
                return@withContext Fuel.post(url)
                    .set("Content-Type", "text/plain")
                    .timeout(Int.MAX_VALUE).timeoutRead(Int.MAX_VALUE)
                    .body(doc!!).responseObject<TextModel>()
            }
            status = response.statusCode
            this.response = result.get()

    }

}
