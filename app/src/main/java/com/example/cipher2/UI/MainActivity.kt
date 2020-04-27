package com.example.cipher2.UI

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.cipher2.FILE_PICKER_REQUEST_CODE
import com.example.cipher2.R
import com.example.cipher2.controllers.DocxCipherRequest
import com.example.cipher2.controllers.ICipherRequest
import com.example.cipher2.controllers.StringCipherRequest
import com.example.cipher2.controllers.TxtCipherRequest
import com.example.cipher2.defaultKey
import com.example.cipher2.exceptions.ApplicationException
import com.example.cipher2.exceptions.NoFileUploadException
import com.example.cipher2.exceptions.NoKeyException
import com.example.cipher2.models.TextModel
import kotlinx.android.synthetic.main.activity_main.*
import kotlinx.coroutines.GlobalScope
import kotlinx.coroutines.MainScope
import kotlinx.coroutines.launch


class MainActivity : AppCompatActivity() {

    var fileUriToUpload: Uri? = null
    var fileNameToSave: String? = null
    var fileTypeToSave: String? = "txt"

    var model: TextModel = TextModel("", "", "")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        try {
            switchMode.isChecked = true
            switchDefaultKey.isChecked = true
            editTextKey.isEnabled = !switchDefaultKey.isChecked
            formatRadioGroup.check(stringRadioButton.id)
            buttonUploadFile.isEnabled = false
            sourceEitText.isEnabled = !buttonUploadFile.isEnabled

            formatRadioGroup.setOnCheckedChangeListener { _, checkedId ->
                when (checkedId) {
                    docxRadioButton.id -> buttonUploadFile.isEnabled = true
                    txtRadioButton.id -> buttonUploadFile.isEnabled = true
                    stringRadioButton.id -> buttonUploadFile.isEnabled = false
                }
                sourceEitText.isEnabled = !buttonUploadFile.isEnabled
            }

            switchMode.setOnCheckedChangeListener { _, isChecked ->
                if (isChecked) {
                    switchMode.text = resources.getText(R.string.title_decrypt)
                    buttonSendRequest.text = resources.getText(R.string.send_decrypt)
                } else {
                    switchMode.text = resources.getText(R.string.title_encrypt)
                    buttonSendRequest.text = resources.getText(R.string.send_encrypt)
                }
            }

            switchDefaultKey.setOnCheckedChangeListener { _, isChecked ->
                editTextKey.isEnabled = !isChecked
            }

            buttonUploadFile.setOnClickListener {
                pickFile()
            }

            buttonSendRequest.setOnClickListener {
                sendRequest()
            }

        } catch (e: ApplicationException) {
            e.toast(this)
        }
    }

    override fun onActivityResult(
        requestCode: Int, resultCode: Int,
        resultData: Intent?
    ) {
        super.onActivityResult(requestCode, resultCode, resultData)
        when (requestCode) {
            FILE_PICKER_REQUEST_CODE -> fileUriToUpload = resultData?.data

        }
    }

    private fun sendRequest() {
        try {
            buttonSendRequest.isEnabled = false
            var request: ICipherRequest? = null
            val key =
                if (switchDefaultKey.isChecked)
                    defaultKey
                else editTextKey.text.toString()
            if (key.isEmpty()) throw NoKeyException()

            when (formatRadioGroup.checkedRadioButtonId) {
                docxRadioButton.id -> {
                    if (fileUriToUpload == null) throw NoFileUploadException()
                    val inStream = contentResolver?.openInputStream(fileUriToUpload!!)
                    request = DocxCipherRequest(key, inStream)
                }
                txtRadioButton.id -> {
                    if (fileUriToUpload == null) throw NoFileUploadException()
                    val inStream = contentResolver?.openInputStream(fileUriToUpload!!)
                    request = TxtCipherRequest(key, inStream)
                }
                stringRadioButton.id -> {
                    val text = sourceEitText.text.toString()
                    request = StringCipherRequest(key, text)
                }
            }


            GlobalScope.launch {
                try {
                    if (switchMode.isChecked) {
                        request?.sendDecrypt()
                    } else {
                        request?.sendEncrypt()
                    }
                    model = request!!.response

                    MainScope().launch {
                        SuccessResultDialog(
                            this@MainActivity,
                            model
                        ).show()
                        buttonSendRequest.isEnabled = true

                    }


                } catch (e: ApplicationException) {
                    MainScope().launch {
                        e.toast(this@MainActivity)
                    }
                } finally {
                    MainScope().launch {
                        buttonSendRequest.isEnabled = true
                    }
                }

            }
        } catch (e: ApplicationException) {
            e.toast(this)
        } finally {
            buttonSendRequest.isEnabled = true

        }
    }

    private fun pickFile() {
        val intent = Intent(Intent.ACTION_OPEN_DOCUMENT)
        intent.addCategory(Intent.CATEGORY_OPENABLE)
        intent.type = "*/*"
        startActivityForResult(intent, FILE_PICKER_REQUEST_CODE)
    }


}
